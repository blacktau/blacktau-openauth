namespace Blacktau.OpenAuth.AspNet.SessionStateStorage
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    public static class OpenAuthorizationServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAuthorizationSessionStorage(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IStateStorageManager, SessionStateStorageManager>();

            return serviceCollection;
        }
    }
}