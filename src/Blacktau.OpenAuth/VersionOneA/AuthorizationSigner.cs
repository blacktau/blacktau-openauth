namespace Blacktau.OpenAuth.VersionOneA
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Blacktau.OpenAuth.Interfaces.VersionOneA;

    public class AuthorizationSigner : IAuthorizationSigner
    {
        public string GetSignature(string applicationSecret, string accessTokenSecret, string url, string method, params IEnumerable<KeyValuePair<string, string>>[] parameters)
        {
            if (string.IsNullOrWhiteSpace(applicationSecret))
            {
                throw new ArgumentNullException(nameof(applicationSecret));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentNullException(nameof(method));
            }

            var allParameters = CollateParameters(parameters);

            var parametersString = ConvertParametersToQueryString(allParameters);
            var signatureBaseString = GenerateBaseSignatureString(method, url, parametersString);
            var signingKey = GetSigningKey(applicationSecret, accessTokenSecret);
            var signature = Sign(signatureBaseString, signingKey);

            return signature;
        }

        private static Dictionary<string, string> CollateParameters(IEnumerable<KeyValuePair<string, string>>[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return parameters.SelectMany(p => p).ToDictionary(p => p.Key, p => p.Value);
        }

        private static string ConvertParametersToQueryString(IDictionary<string, string> allParameters)
        {
            var parameters = allParameters.OrderBy(p => p.Key);
            var encodedParameterPairs =
                parameters.Select(a => new { a, encodedkey = a.Key.UrlEncode() })
                    .Select(b => new { b, encodedValue = b.a.Value.UrlEncode() })
                    .Select(c => c.b.encodedkey + "=" + c.encodedValue)
                    .OrderBy(encodedPair => encodedPair)
                    .Select(encodedPair => encodedPair);

            var result = string.Join("&", encodedParameterPairs.ToArray());
            return result;
        }

        private static string GenerateBaseSignatureString(string method, string url, string parametersString)
        {
            return method.ToUpper() + "&" + url.UrlEncode() + "&" + parametersString.UrlEncode();
        }

        private static string GetSigningKey(string applicationSecret, string accessTokenSecret)
        {
            return applicationSecret + "&" + (accessTokenSecret ?? string.Empty);
        }

        private static string Sign(string signatureBaseString, string signingKey)
        {
            var keyBytes = Encoding.ASCII.GetBytes(signingKey);
            using (var myhmacsha1 = new HMACSHA1(keyBytes))
            {
                var byteArray = Encoding.ASCII.GetBytes(signatureBaseString);
                var stream = new MemoryStream(byteArray);
                var signedValue = myhmacsha1.ComputeHash(stream);
                var result = Convert.ToBase64String(signedValue);
                return result;
            }
        }
    }
}