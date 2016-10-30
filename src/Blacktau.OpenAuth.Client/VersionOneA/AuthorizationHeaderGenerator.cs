namespace Blacktau.OpenAuth.Client.VersionOneA
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;

    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.Interfaces.VersionOneA;

    public class AuthorizationHeaderGenerator : IOpenAuthVersionOneAAuthorizationHeaderGenerator
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

        public AuthenticationHeaderValue GenerateHeaderValue(IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation, IOpenAuthClient openAuthClient)
        {
            if (applicationCredentials == null)
            {
                throw new ArgumentNullException(nameof(applicationCredentials));
            }
/*
            if (authorizationInformation == null)
            {
                throw new ArgumentNullException(nameof(authorizationInformation));
            }*/

            if (openAuthClient == null)
            {
                throw new ArgumentNullException(nameof(openAuthClient));
            }

            var queryParameters = openAuthClient.QueryParameters;
            var bodyParameters = openAuthClient.BodyParameters;
            var authorizationHeaderParameters = openAuthClient.AuthorizationHeaderParameters;

            var authorizationParameters = this.GetAuthorisationParameters(applicationCredentials, authorizationInformation?.AccessToken, authorizationHeaderParameters);

            var signature = this.GetSignature(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, openAuthClient.Method, openAuthClient.Url, authorizationParameters);

            authorizationParameters.Add(AuthorizationFieldNames.Signature, signature);

            var header = GetAuthorisationHeader(authorizationParameters);

            return header;
        }

        private static AuthenticationHeaderValue GetAuthorisationHeader(IDictionary<string, string> authorizationParameters)
        {
            var authorizationHeader = authorizationParameters.OrderBy(p => p.Key).Select(GetParameterPair).ToArray();
            var joinedHeader = string.Join(",", authorizationHeader);

            return new AuthenticationHeaderValue(AuthorizationFieldNames.AuthorizationHeaderStart, joinedHeader);
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

        private IDictionary<string, string> GetAuthorisationParameters(IApplicationCredentials applicationCredentials, string accessToken, IReadOnlyDictionary<string, string> additionalAuthorizationParameters)
        {
            var parameters = this.parametersGenerator.GetAuthorizationParameters(applicationCredentials, accessToken);

            foreach (var additionalAuthorizationParameter in additionalAuthorizationParameters)
            {
                parameters.Add(additionalAuthorizationParameter.Key, additionalAuthorizationParameter.Value);
            }

            return parameters;
        }

        private string GetSignature(
            IApplicationCredentials applicationCredentials,
            IEnumerable<KeyValuePair<string, string>> queryParameters,
            IEnumerable<KeyValuePair<string, string>> bodyParameters,
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
                authorizationInformation?.AccessTokenSecret,
                urlWithoutQuery,
                methodText,
                authorizationParameters,
                queryParameters,
                bodyToInclude);

            return signature;
        }
    }
}