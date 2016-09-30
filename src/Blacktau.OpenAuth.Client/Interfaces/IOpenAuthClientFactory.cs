namespace Blacktau.OpenAuth.Client.Interfaces
{
    public interface IOpenAuthClientFactory
    {
        IOpenAuthClient CreateOpenAuthClient(string baseUrl, HttpMethod method, OpenAuthVersion openAuthVersion, IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation);
    }
}