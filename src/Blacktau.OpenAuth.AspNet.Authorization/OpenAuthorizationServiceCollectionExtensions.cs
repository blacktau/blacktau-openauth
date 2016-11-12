namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo;
    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;
    using Blacktau.OpenAuth.AspNet.Authorization.VersionTwo;
    using Blacktau.OpenAuth.Client.Containers.ServiceCollection;
    using Blacktau.OpenAuth.Client.VersionTwo;

    using Microsoft.Extensions.DependencyInjection;

    public static class OpenAuthorizationServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddBlacktauOpenAuthClient();

            serviceCollection.AddSingleton<IOpenAuthorizationHandlerFactory, OpenAuthorizationHandlerFactory>();
            serviceCollection.AddSingleton<IVersionOneAAuthorizationRequestor, VersionOneAAuthorizationRequestor>();
            serviceCollection.AddSingleton<IVersionTwoAuthorizationRequestor, VersionTwoAuthorizationRequestor>();
            serviceCollection.AddSingleton<IOpenAuthorizationResourceProviderRegistery, OpenAuthorizationResourceProviderRegistery>();
            serviceCollection.AddSingleton<IRequestTokenStorageManager, RequestTokenStorageManager>();
            serviceCollection.AddSingleton<IVersionOneACallbackHandler, VersionOneACallbackHandler>();
            serviceCollection.AddSingleton<IUrlValidator, UrlValidator>();
            serviceCollection.AddSingleton<IVersionTwoAuthorizationParametersGenerator, VersionTwoAuthorizationParametersGenerator>();
            serviceCollection.AddSingleton<IOpenAuthorizationTwoHandlerFactory, OpenAuthorizationTwoHandlerFactory>();
            serviceCollection.AddSingleton<IVersionTwoCallbackHandler, VersionTwoCallbackHandler>();
            serviceCollection.AddSingleton<IOpenAuthorizationOneAHandlerFactory, OpenAuthorizationOneAHandlerFactory>();

            return serviceCollection;
        }
    }
}