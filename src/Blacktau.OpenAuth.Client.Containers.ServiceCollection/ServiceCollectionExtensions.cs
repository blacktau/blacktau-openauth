namespace Blacktau.OpenAuth.IOC.ServiceCollection
{
    using Blacktau.OpenAuth.Interfaces;
    using Blacktau.OpenAuth.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.VersionOneA;
    using Blacktau.OpenAuth.VersionTwo;

    using Microsoft.Extensions.DependencyInjection;

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
            serviceCollection.AddSingleton<IOpenAuthVersionOneAAuthorizationHeaderGenerator, VersionOneA.AuthorizationHeaderGenerator>();
            serviceCollection.AddSingleton<IOpenAuthVersionTwoAuthorizationHeaderGenerator, VersionTwo.AuthorizationHeaderGenerator>();
            serviceCollection.AddSingleton<IOpenAuthClientFactory, OpenAuthClientFactory>();

            return serviceCollection;
        }
    }
}