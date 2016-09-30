namespace Blacktau.OpenAuth.Client.Interfaces
{
    using System.Net.Http;

    public interface IHttpClientFactory
    {
        IHttpClient CreateHttpClient(HttpMessageHandler handler);
    }
}