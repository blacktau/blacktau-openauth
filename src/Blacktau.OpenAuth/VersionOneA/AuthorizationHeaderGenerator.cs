namespace Blacktau.OpenAuth.VersionOneA
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Blacktau.OpenAuth.Interfaces;
    using Blacktau.OpenAuth.Interfaces.VersionOneA;

    public class AuthorizationHeaderGenerator : IAuthorizationHeaderGenerator
    {
        private readonly IAuthorizationSigner authorizationSigner;

        private readonly IAuthorizationParametersGenerator parametersGenerator;

        public AuthorizationHeaderGenerator(IAuthorizationParametersGenerator parametersGenerator, IAuthorizationSigner authorizationSigner)
        {
            if (parametersGenerator == null)
            {
                throw new ArgumentNullException(nameof(parametersGenerator));
            }

            if (authorizationSigner == null)
            {
                throw new ArgumentNullException(nameof(authorizationSigner));
            }

            this.parametersGenerator = parametersGenerator;
            this.authorizationSigner = authorizationSigner;
        }

        public string GenerateHeaderValue(
            IApplicationCredentials applicationCredentials,
            IDictionary<string, string> queryParameters,
            IDictionary<string, string> bodyParameters,
            IAuthorizationInformation authorizationInformation,
            HttpMethod method,
            string url)
        {
            if (applicationCredentials == null)
            {
                throw new ArgumentNullException(nameof(applicationCredentials));
            }

            if (queryParameters == null)
            {
                throw new ArgumentNullException(nameof(queryParameters));
            }

            if (bodyParameters == null)
            {
                throw new ArgumentNullException(nameof(bodyParameters));
            }

            if (authorizationInformation == null)
            {
                throw new ArgumentNullException(nameof(authorizationInformation));
            }

            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            var authorizationParameters = this.GetAuthorisationParameters(applicationCredentials, authorizationInformation.AccessToken);

            var signature = this.GetSignature(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, method, url, authorizationParameters);

            authorizationParameters.Add(AuthorizationFieldNames.Signature, signature);

            var header = GetAuthorisationHeader(authorizationParameters);

            return header;
        }

        private static string GetAuthorisationHeader(IDictionary<string, string> authorizationParameters)
        {
            var authorizationHeader = authorizationParameters.OrderBy(p => p.Key).Select(GetParameterPair).ToArray();
            return string.Join(",", authorizationHeader);
        }

        private static string GetEncodedValue(string parameter)
        {
            return parameter.UrlEncode();
        }

        private static string GetMethodAsString(HttpMethod method)
        {
            return method == HttpMethod.Get ? "GET" : "POST";
        }

        private static string GetParameterPair(KeyValuePair<string, string> pair)
        {
            var encodedValue = GetEncodedValue(pair.Value);
            return $"{pair.Key}=\"{encodedValue}\"";
        }

        private static string GetUrlWithoutQuery(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;
            }

            var parts = url.Split('?');

            if (parts.Length == 0)
            {
                return string.Empty;
            }

            var part = Uri.EscapeUriString(parts[0]);
            return part;
        }

        private IDictionary<string, string> GetAuthorisationParameters(IApplicationCredentials applicationCredentials, string accessToken)
        {
            return this.parametersGenerator.GetAuthorizationParameters(applicationCredentials, accessToken);
        }

        private string GetSignature(
            IApplicationCredentials applicationCredentials,
            IDictionary<string, string> queryParameters,
            IDictionary<string, string> bodyParameters,
            IAuthorizationInformation authorizationInformation,
            HttpMethod method,
            string url,
            IDictionary<string, string> authorizationParameters)
        {
            var urlWithoutQuery = GetUrlWithoutQuery(url);
            var methodText = GetMethodAsString(method);

            var bodyToInclude = method == HttpMethod.Post ? bodyParameters : new Dictionary<string, string>(0);

            var signature = this.authorizationSigner.GetSignature(
                applicationCredentials.ApplicationSecret,
                authorizationInformation.AccessTokenSecret,
                urlWithoutQuery,
                methodText,
                authorizationParameters,
                queryParameters,
                bodyToInclude);

            return signature;
        }
    }
}