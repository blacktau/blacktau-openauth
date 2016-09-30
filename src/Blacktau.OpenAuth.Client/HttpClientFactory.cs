namespace Blacktau.OpenAuth.Client
{
    using System.Net.Http;

    using Blacktau.OpenAuth.Client.Interfaces;

    public class HttpClientFactory : IHttpClientFactory
    {
        public IHttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return new HttpClient(handler);
        }
    }
}