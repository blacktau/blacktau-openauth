namespace Blacktau.OpenAuth.Basic
{
    using Blacktau.OpenAuth.Interfaces;
    using Blacktau.OpenAuth.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.VersionOneA;

    public class OpenAuthClientFactory : IOpenAuthClientFactory
    {
        private readonly IApplicationCredentials applicationCredentials;

        private readonly IAuthorizationHeaderGenerator authorizationHeaderGenerator;

        private readonly IAuthorizationInformation authorizationInformation;

        private readonly IHttpClientFactory httpClientFactory;

        public OpenAuthClientFactory(IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation)
        {
            this.applicationCredentials = applicationCredentials;
            this.authorizationInformation = authorizationInformation;

            var clock = this.CreateClock();
            var timeStampFactory = this.CreateTimeStampFactory(clock);
            var nonceFactory = this.CreateNonceFactory();

            var parametersGenerator = this.CreateAuthorizationParametersGenerator(nonceFactory, timeStampFactory);
            var authorizationSigner = this.CreateAuthorizationSigner();


            this.httpClientFactory = this.CreateHttpClientFactory();
            this.authorizationHeaderGenerator = this.CreateAuthorizationHeaderGenerator(parametersGenerator, authorizationSigner);
        }

        public IOpenAuthClient CreateOpenAuthClient(string baseUrl, HttpMethod method, OpenAuthVersion openAuthVersion)
        {
            return new OpenAuthClient(baseUrl, method, this.applicationCredentials, this.authorizationHeaderGenerator, this.authorizationInformation, this.httpClientFactory);
        }

        private IAuthorizationHeaderGenerator CreateAuthorizationHeaderGenerator(IAuthorizationParametersGenerator parametersGenerator, IAuthorizationSigner authorizationSigner)
        {
            return new AuthorizationHeaderGenerator(parametersGenerator, authorizationSigner);
        }

        private IAuthorizationParametersGenerator CreateAuthorizationParametersGenerator(INonceFactory nonceFactory, ITimeStampFactory timeStampFactory)
        {
            return new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);
        }

        private IAuthorizationSigner CreateAuthorizationSigner()
        {
            return new AuthorizationSigner();
        }

        private IClock CreateClock()
        {
            return new SystemClock();
        }

        private IHttpClientFactory CreateHttpClientFactory()
        {
            return new HttpClientFactory();
        }

        private INonceFactory CreateNonceFactory()
        {
            return new NonceFactory();
        }

        private ITimeStampFactory CreateTimeStampFactory(IClock clock)
        {
            return new TimeStampFactory(clock);
        }
    }
}