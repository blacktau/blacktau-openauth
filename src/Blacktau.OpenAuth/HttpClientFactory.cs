namespace Blacktau.OpenAuth
{
    using System.Net.Http;

    using Blacktau.OpenAuth.Interfaces;
    public class HttpClientFactory : IHttpClientFactory
    {
        public IHttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return new HttpClient(handler);
        }

    }
}