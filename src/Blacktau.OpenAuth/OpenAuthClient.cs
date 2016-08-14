namespace Blacktau.OpenAuth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Interfaces;
    using Blacktau.OpenAuth.VersionOneA;

    public class OpenAuthClient : IOpenAuthClient
    {
        private readonly IApplicationCredentials applicationCredentials;

        private readonly IAuthorizationHeaderGenerator authHeaderGenerator;

        private readonly IAuthorizationInformation authorizationInformation;

        private readonly IHttpClientFactory httpClientFactory;

        private IHttpClient client;

        public OpenAuthClient(
            string url,
            HttpMethod method,
            IApplicationCredentials applicationCredentials,
            IAuthorizationHeaderGenerator authHeaderGenerator,
            IAuthorizationInformation authorizationInformation, 
            IHttpClientFactory httpClientFactory)
        {
            if (applicationCredentials == null)
            {
                throw new ArgumentNullException(nameof(applicationCredentials));
            }

            if (authHeaderGenerator == null)
            {
                throw new ArgumentNullException(nameof(authHeaderGenerator));
            }

            if (authorizationInformation == null)
            {
                throw new ArgumentNullException(nameof(authorizationInformation));
            }

            if (httpClientFactory == null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            
            this.applicationCredentials = applicationCredentials;
            this.authHeaderGenerator = authHeaderGenerator;
            this.authorizationInformation = authorizationInformation;
            this.httpClientFactory = httpClientFactory;
            this.Url = url;
            this.Method = method;

            this.QueryParameters = new Dictionary<string, string>();
            this.BodyParameters = new Dictionary<string, string>();

            this.CreateHttpClient();
        }

        private void CreateHttpClient()
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None,
                PreAuthenticate = true
            };

            this.client = this.httpClientFactory.CreateHttpClient(handler);

            this.client.DefaultRequestHeaders.ExpectContinue = false;
        }

        public HttpMethod Method { get; set; }

        public string Url { get; set; }

        internal Dictionary<string, string> BodyParameters { get; }

        internal Dictionary<string, string> QueryParameters { get; }

        public async Task<string> Execute()
        {
            this.ValidateRequest();

            this.client.DefaultRequestHeaders.Authorization = this.CreateNewAuthorizationHeader();

            var fullUrl = this.CreatFullUrl();

            switch (this.Method)
            {
                case HttpMethod.Get:

                    return await this.MakeGetRequest(fullUrl);

                case HttpMethod.Post:

                    return await this.MakePostRequest(fullUrl);
            }

            throw new Exception("Unexpected HttpMethod");
        }

        private void ValidateRequest()
        {
            if (string.IsNullOrWhiteSpace(this.Url))
            {
                throw new InvalidOperationException("Url must be specified.");
            }
        }

        private async Task<string> MakePostRequest(Uri fullUrl)
        {
            var queryString = this.ToQueryString(this.BodyParameters);
            var content = new StringContent(queryString, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var result = await this.client.PostAsync(fullUrl, content);
            return await result.Content.ReadAsStringAsync();
        }

        private async Task<string> MakeGetRequest(Uri fullUrl)
        {
            return await this.client.GetStringAsync(fullUrl);
        }
        
        public void AddQueryParameter(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("invalid value.", nameof(name));
            }

            this.QueryParameters.Add(name, value);
        }

        public void AddBodyParameter(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("invalid value.", nameof(name));
            }

            this.BodyParameters.Add(name, value);
        }

        public void ClearParameters()
        {
            this.QueryParameters.Clear();
            this.BodyParameters.Clear();
        }

        private Uri CreatFullUrl()
        {
            if (this.QueryParameters.Count == 0)
            {
                return new Uri(this.Url);
            }

            var queryString = this.ToQueryString(this.QueryParameters);

            return new Uri(new Uri(this.Url), ("?" + queryString));
        }

        private string ToQueryString(Dictionary<string, string> dictionary)
        {
            var query = dictionary.Select(kv => string.Format("{0}={1}", kv.Key, kv.Value)).Aggregate(string.Empty, (q, next) => q + next + "&");
            if (string.IsNullOrEmpty(query))
            {
                return string.Empty;
            }

            return query.Substring(0, query.Length - 1);
        }

        private AuthenticationHeaderValue CreateNewAuthorizationHeader()
        {
            var headerValue = this.authHeaderGenerator.GenerateHeaderValue(this.applicationCredentials, this.QueryParameters, this.BodyParameters, this.authorizationInformation, this.Method, this.Url);
            return new AuthenticationHeaderValue(AuthorizationFieldNames.AuthorizationHeaderStart, headerValue);
        }
    }
}