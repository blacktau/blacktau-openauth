namespace Blacktau.OpenAuth.Basic
{
    using System;

    using Blacktau.OpenAuth.Interfaces;
    using Blacktau.OpenAuth.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.VersionOneA;

    public class OpenAuthClientFactory : IOpenAuthClientFactory
    {
        private readonly IApplicationCredentials applicationCredentials;

        private readonly IAuthorizationHeaderGenerator versionOneAAuthorizationHeaderGenerator;

        private readonly IAuthorizationInformation authorizationInformation;

        private readonly IHttpClientFactory httpClientFactory;

        private readonly IAuthorizationHeaderGenerator versionTwoAuthorizationHeaderGenerator;

        public OpenAuthClientFactory(IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation)
        {
            this.applicationCredentials = applicationCredentials;
            this.authorizationInformation = authorizationInformation;

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

        public IOpenAuthClient CreateOpenAuthClient(string baseUrl, HttpMethod method, OpenAuthVersion openAuthVersion)
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

            return new OpenAuthClient(baseUrl, method, this.applicationCredentials, authorizationHeaderGenerator, this.authorizationInformation, this.httpClientFactory);
        }

        private static IAuthorizationHeaderGenerator CreateVersionOneAAuthorizationHeaderGenerator(IAuthorizationParametersGenerator parametersGenerator, IAuthorizationSigner authorizationSigner)
        {
            return new VersionOneA.AuthorizationHeaderGenerator(parametersGenerator, authorizationSigner);
        }

        private static IAuthorizationHeaderGenerator CreateVersionTwoAuthorizationHeaderGenerator()
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