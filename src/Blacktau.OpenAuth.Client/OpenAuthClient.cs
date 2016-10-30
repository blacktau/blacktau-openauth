namespace Blacktau.OpenAuth.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Interfaces;

    public class OpenAuthClient : IOpenAuthClient
    {
        private readonly Dictionary<string, string> additionalAuthorizationHeaderParameters;

        private readonly IApplicationCredentials applicationCredentials;

        private readonly IAuthorizationHeaderGenerator authHeaderGenerator;

        private readonly IAuthorizationInformation authorizationInformation;

        private readonly Dictionary<string, string> bodyParameters;

        private readonly IHttpClientFactory httpClientFactory;

        private readonly Dictionary<string, string> queryParameters;

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

            this.queryParameters = new Dictionary<string, string>();
            this.bodyParameters = new Dictionary<string, string>();
            this.additionalAuthorizationHeaderParameters = new Dictionary<string, string>();

            this.CreateHttpClient();
        }

        public IReadOnlyDictionary<string, string> AuthorizationHeaderParameters => new ReadOnlyDictionary<string, string>(this.additionalAuthorizationHeaderParameters);

        public IReadOnlyDictionary<string, string> BodyParameters => new ReadOnlyDictionary<string, string>(this.bodyParameters);

        public HttpMethod Method { get; set; }

        public IReadOnlyDictionary<string, string> QueryParameters => new ReadOnlyDictionary<string, string>(this.queryParameters);

        public string Url { get; set; }

        public void AddAdditionalAuthorizationParameter(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.Exception_InvalidAdditionalAuthorizationHeaderParameterName);
            }

            this.additionalAuthorizationHeaderParameters.Add(name, value);
        }

        public void AddBodyParameter(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.Exception_InvalidBodyParameterName, nameof(name));
            }

            this.bodyParameters.Add(name, value);
        }

        public void AddQueryParameter(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.Exception_InvalidQueryParameterName);
            }

            this.queryParameters.Add(name, value);
        }

        public void ClearParameters()
        {
            this.queryParameters.Clear();
            this.bodyParameters.Clear();
            this.additionalAuthorizationHeaderParameters.Clear();
        }

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

            throw new Exception(Resources.Exception_UnexpectedHttpMethod);
        }

        private void CreateHttpClient()
        {
            var handler = new HttpClientHandler { AllowAutoRedirect = true, AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None, PreAuthenticate = true };

            this.client = this.httpClientFactory.CreateHttpClient(handler);

            this.client.DefaultRequestHeaders.ExpectContinue = false;
        }

        private AuthenticationHeaderValue CreateNewAuthorizationHeader()
        {
            return this.authHeaderGenerator.GenerateHeaderValue(this.applicationCredentials, this.authorizationInformation, this);
        }

        private Uri CreatFullUrl()
        {
            if (this.QueryParameters.Count == 0)
            {
                return new Uri(this.Url);
            }

            var queryString = this.queryParameters.ToQueryString();

            return new Uri(new Uri(this.Url), UriConstants.QuestionMarkDelimiter + queryString);
        }

        private async Task<string> MakeGetRequest(Uri fullUrl)
        {
            return await this.client.GetStringAsync(fullUrl);
        }

        private async Task<string> MakePostRequest(Uri fullUrl)
        {
            var queryString = this.bodyParameters.ToQueryString();
            var content = new StringContent(queryString, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var result = await this.client.PostAsync(fullUrl, content);
            return await result.Content.ReadAsStringAsync();
        }

        private void ValidateRequest()
        {
            if (string.IsNullOrWhiteSpace(this.Url))
            {
                throw new InvalidOperationException(string.Format(Resources.Exception_IsRequired, nameof(this.Url)));
            }
        }
    }
}