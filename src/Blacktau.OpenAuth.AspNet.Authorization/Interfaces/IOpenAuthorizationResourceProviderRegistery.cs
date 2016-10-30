namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    using System.Collections.Generic;

    public interface IOpenAuthorizationResourceProviderRegistery
    {
        IEnumerable<OAuthResourceProviderDescription> GetConfiguredResourceProviders();

        void Register(OpenAuthorizationOptions options);
    }
}