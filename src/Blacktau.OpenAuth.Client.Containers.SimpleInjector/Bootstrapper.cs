namespace Blacktau.OpenAuth.Client.Containers.SimpleInjector
{
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using global::SimpleInjector;

    public class Bootstrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IClock, SystemClock>(Lifestyle.Singleton);
            container.Register<IHttpClientFactory, HttpClientFactory>(Lifestyle.Singleton);
            container.Register<INonceFactory, NonceFactory>(Lifestyle.Singleton);
            container.Register<ITimeStampFactory, TimeStampFactory>(Lifestyle.Singleton);
            container.Register<IAuthorizationSigner, AuthorizationSigner>(Lifestyle.Singleton);
            container.Register<IAuthorizationParametersGenerator, AuthorizationParametersGenerator>(Lifestyle.Singleton);
            container.Register<IOpenAuthVersionOneAAuthorizationHeaderGenerator, VersionOneA.AuthorizationHeaderGenerator>();
            container.Register<IOpenAuthVersionTwoAuthorizationHeaderGenerator, VersionTwo.AuthorizationHeaderGenerator>();
            container.Register<IOpenAuthClientFactory, OpenAuthClientFactory>(Lifestyle.Singleton);
        }
    }
}