namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;
    using Blacktau.OpenAuth.Client.Containers.ServiceCollection;

    using Microsoft.Extensions.DependencyInjection;

    public static class OpenAuthorizationServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddBlacktauOpenAuthClient();

            serviceCollection.AddSingleton<IOpenAuthorizationHandlerFactory, OpenAuthorizationHandlerFactory>();
            serviceCollection.AddSingleton<IAuthorizationRequestor, AuthorizationRequestor>();
            serviceCollection.AddSingleton<IOpenAuthorizationResourceProviderRegistery, OpenAuthorizationResourceProviderRegistery>();
            serviceCollection.AddSingleton<IRequestTokenStorageManager, RequestTokenStorageManager>();
            serviceCollection.AddSingleton<ICallbackHandler, CallbackHandler>();

            return serviceCollection;
        }
    }
}