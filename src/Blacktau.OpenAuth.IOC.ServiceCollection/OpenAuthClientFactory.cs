namespace Blacktau.OpenAuth.IOC.ServiceCollection
{
    using System;

    using Blacktau.OpenAuth.Interfaces;

    public class OpenAuthClientFactory : IOpenAuthClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;

        private readonly IOpenAuthVersionOneAAuthorizationHeaderGenerator versionOneAAuthorizationHeaderGenerator;

        private readonly IOpenAuthVersionTwoAuthorizationHeaderGenerator versionTwoAuthorizationHeaderGenerator;

        public OpenAuthClientFactory(IHttpClientFactory httpClientFactory, IOpenAuthVersionOneAAuthorizationHeaderGenerator versionOneAAuthorizationHeaderGenerator, IOpenAuthVersionTwoAuthorizationHeaderGenerator versionTwoAuthorizationHeaderGenerator)
        {
            if (httpClientFactory == null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }

            if (versionOneAAuthorizationHeaderGenerator == null)
            {
                throw new ArgumentNullException(nameof(versionOneAAuthorizationHeaderGenerator));
            }

            if (versionTwoAuthorizationHeaderGenerator == null)
            {
                throw new ArgumentNullException(nameof(versionTwoAuthorizationHeaderGenerator));
            }

            this.httpClientFactory = httpClientFactory;
            this.versionOneAAuthorizationHeaderGenerator = versionOneAAuthorizationHeaderGenerator;
            this.versionTwoAuthorizationHeaderGenerator = versionTwoAuthorizationHeaderGenerator;
        }

        public IOpenAuthClient CreateOpenAuthClient(string baseUrl, HttpMethod method, OpenAuthVersion openAuthVersion, IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation)
        {
            IAuthorizationHeaderGenerator headerGenerator;

            if (openAuthVersion == OpenAuthVersion.OneA)
            {
                headerGenerator = this.versionOneAAuthorizationHeaderGenerator;
            }
            else if (openAuthVersion == OpenAuthVersion.Two)
            {
                headerGenerator = this.versionTwoAuthorizationHeaderGenerator;
            }
            else
            {
                throw new Exception("Invalid OpenAuthVersion");
            }

            return new OpenAuthClient(baseUrl, method, applicationCredentials, headerGenerator, authorizationInformation, this.httpClientFactory);
        }
    }
}