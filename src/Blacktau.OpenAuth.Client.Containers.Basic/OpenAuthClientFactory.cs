namespace Blacktau.OpenAuth.Client.Containers.Basic
{
    using System;

    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client.VersionOneA;

    public class OpenAuthClientFactory : IOpenAuthClientFactory
    {
        private readonly IOpenAuthVersionOneAAuthorizationHeaderGenerator versionOneAAuthorizationHeaderGenerator;

        private readonly IHttpClientFactory httpClientFactory;

        private readonly IOpenAuthVersionTwoAuthorizationHeaderGenerator versionTwoAuthorizationHeaderGenerator;

        public OpenAuthClientFactory()
        {
            // This code could be refactored but won't be because it serves to illustrate the dependencies in creating a versionOne AuthorizationHeaderGenerator
            var clock = CreateClock();
            var timeStampFactory = CreateTimeStampFactory(clock);
            var nonceFactory = CreateNonceFactory();

            var parametersGenerator = CreateAuthorizationParametersGenerator(nonceFactory, timeStampFactory);
            var authorizationSigner = CreateAuthorizationSigner();

            this.versionOneAAuthorizationHeaderGenerator = CreateVersionOneAAuthorizationHeaderGenerator(parametersGenerator, authorizationSigner);

            this.httpClientFactory = CreateHttpClientFactory();
            this.versionTwoAuthorizationHeaderGenerator = CreateVersionTwoAuthorizationHeaderGenerator();
        }

        public IOpenAuthClient CreateOpenAuthClient(string baseUrl, HttpMethod method, OpenAuthVersion openAuthVersion, IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation)
        {
            IAuthorizationHeaderGenerator authorizationHeaderGenerator;

            switch (openAuthVersion)
            {
                case OpenAuthVersion.OneA:
                    authorizationHeaderGenerator = this.versionOneAAuthorizationHeaderGenerator;
                    break;

                case OpenAuthVersion.Two:
                    authorizationHeaderGenerator = this.versionTwoAuthorizationHeaderGenerator;
                    break;

                default:
                    throw new ArgumentException("Unsupported OpenAuthVersion.", nameof(openAuthVersion));
            }

            return new OpenAuthClient(baseUrl, method, applicationCredentials, authorizationHeaderGenerator, authorizationInformation, this.httpClientFactory);
        }

        private static IOpenAuthVersionOneAAuthorizationHeaderGenerator CreateVersionOneAAuthorizationHeaderGenerator(IAuthorizationParametersGenerator parametersGenerator, IAuthorizationSigner authorizationSigner)
        {
            return new VersionOneA.AuthorizationHeaderGenerator(parametersGenerator, authorizationSigner);
        }

        private static IOpenAuthVersionTwoAuthorizationHeaderGenerator CreateVersionTwoAuthorizationHeaderGenerator()
        {
            return new VersionTwo.AuthorizationHeaderGenerator();
        }

        private static IAuthorizationParametersGenerator CreateAuthorizationParametersGenerator(INonceFactory nonceFactory, ITimeStampFactory timeStampFactory)
        {
            return new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);
        }

        private static IAuthorizationSigner CreateAuthorizationSigner()
        {
            return new AuthorizationSigner();
        }

        private static IClock CreateClock()
        {
            return new SystemClock();
        }

        private static IHttpClientFactory CreateHttpClientFactory()
        {
            return new HttpClientFactory();
        }

        private static INonceFactory CreateNonceFactory()
        {
            return new NonceFactory();
        }

        private static ITimeStampFactory CreateTimeStampFactory(IClock clock)
        {
            return new TimeStampFactory(clock);
        }
    }
}