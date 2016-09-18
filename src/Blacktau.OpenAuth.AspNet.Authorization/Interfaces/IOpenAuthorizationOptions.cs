namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    using Blacktau.OpenAuth.Interfaces;

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