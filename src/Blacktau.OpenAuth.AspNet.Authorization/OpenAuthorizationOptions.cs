namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class OpenAuthorizationOptions : IOpenAuthorizationOptions
    {
        public IApplicationCredentials ApplicationCredentials { get; set; }

        public string RequestTokenEndpointUri { get; set; }

        public string AuthorizeEndpointUri { get; set; }

        public string AccessTokenEndpointUri { get; set; }

        public OpenAuthVersion OpenAuthVersion { get; set; }

        public string ServiceProviderName { get; set; }
    }
}