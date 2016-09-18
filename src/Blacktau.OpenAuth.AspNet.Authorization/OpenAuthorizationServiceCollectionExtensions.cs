namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    public static class OpenAuthorizationServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IOpenAuthorizationHandlerFactory, OpenAuthorizationHandlerFactory>();

            return serviceCollection;
        }
    }
}