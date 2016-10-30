namespace Blacktau.OpenAuth.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public static class QueryParameterExtensions
    {
        public static IDictionary<string, string> QueryParameterStringToDictionary(this string query)
        {
            var keypairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new Dictionary<string, string>();
            foreach (var keypair in keypairs)
            {
                string[] parts = keypair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    string name = parts[0];
                    string val = parts[1];
                    result.Add(name, WebUtility.UrlDecode(val));
                }
            }

            return result;
        }

        public static string ToQueryString(this IDictionary<string, string> dictionary)
        {
            var query = dictionary.Select(kv => string.Format("{0}={1}", kv.Key, kv.Value)).Aggregate(string.Empty, (q, next) => q + next + UriConstants.AmpersandDelimiter);
            if (string.IsNullOrEmpty(query))
            {
                return string.Empty;
            }

            return query.Substring(0, query.Length - 1);
        }
    }
}