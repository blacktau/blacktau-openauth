namespace Blacktau.OpenAuth.Client.Containers.ServiceCollection
{
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using Microsoft.Extensions.DependencyInjection;

    using AuthorizationHeaderGenerator = Blacktau.OpenAuth.Client.VersionOneA.AuthorizationHeaderGenerator;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlacktauOpenAuthClient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IClock, SystemClock>();
            serviceCollection.AddSingleton<IHttpClientFactory, HttpClientFactory>();
            serviceCollection.AddSingleton<INonceFactory, NonceFactory>();
            serviceCollection.AddSingleton<ITimeStampFactory, TimeStampFactory>();
            serviceCollection.AddSingleton<IAuthorizationSigner, AuthorizationSigner>();
            serviceCollection.AddSingleton<IAuthorizationParametersGenerator, AuthorizationParametersGenerator>();
            serviceCollection.AddSingleton<IOpenAuthVersionOneAAuthorizationHeaderGenerator, AuthorizationHeaderGenerator>();
            serviceCollection.AddSingleton<IOpenAuthVersionTwoAuthorizationHeaderGenerator, Client.VersionTwo.AuthorizationHeaderGenerator>();
            serviceCollection.AddSingleton<IOpenAuthClientFactory, OpenAuthClientFactory>();

            return serviceCollection;
        }
    }
}