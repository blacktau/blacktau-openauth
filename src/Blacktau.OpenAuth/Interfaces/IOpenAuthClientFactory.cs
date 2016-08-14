namespace Blacktau.OpenAuth.Interfaces
{
    public interface IOpenAuthClientFactory
    {
        IOpenAuthClient CreateOpenAuthClient(string baseUrl, HttpMethod method, OpenAuthVersion openAuthVersion);
    }
}