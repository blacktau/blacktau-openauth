namespace Blacktau.OpenAuth.Interfaces
{
    using System.Net.Http;

    public interface IHttpClientFactory
    {
        IHttpClient CreateHttpClient(HttpMessageHandler handler);
    }
}