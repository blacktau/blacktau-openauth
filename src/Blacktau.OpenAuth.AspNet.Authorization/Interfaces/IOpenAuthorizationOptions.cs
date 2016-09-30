namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;

    public interface IOpenAuthorizationOptions
    {
        IApplicationCredentials ApplicationCredentials { get; set; }

        string RequestTokenEndpointUri { get; set; }

        string AuthorizeEndpointUri { get; set; }

        string AccessTokenEndpointUri { get; set; }

        OpenAuthVersion OpenAuthVersion { get; set; }

        string ServiceProviderName { get; set; }
    }
}