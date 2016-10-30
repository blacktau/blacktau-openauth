namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Globalization;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;
    using Blacktau.OpenAuth.Client;

    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationHandlerFactory : IOpenAuthorizationHandlerFactory
    {
        private readonly ILoggerFactory loggerFactory;

        private readonly IAuthorizationRequestor authorizationRequestor;

        private readonly ICallbackHandler callbackHandler;

        private readonly ILogger<OpenAuthorizationHandlerFactory> logger;

        public OpenAuthorizationHandlerFactory(ILoggerFactory loggerFactory, IAuthorizationRequestor authorizationRequestor, ICallbackHandler callbackHandler)
        {
            this.loggerFactory = loggerFactory;
            this.authorizationRequestor = authorizationRequestor;
            this.callbackHandler = callbackHandler;

            this.logger = this.loggerFactory.CreateLogger<OpenAuthorizationHandlerFactory>();
        }

        public IOpenAuthorizationHandler CreateHandler(OpenAuthorizationOptions options)
        {
            if (options.OpenAuthVersion == OpenAuthVersion.OneA)
            {
                this.logger.LogInformation("returning handler for OAuth Version 1.0A");
                return new OpenAuthorizationVersionOneAHandler(this.loggerFactory, this.authorizationRequestor, this.callbackHandler, options);
            }

            throw new Exception(string.Format(CultureInfo.CurrentCulture, Client.Resources.Exception_Invalid, options.OpenAuthVersion));
        }
    }
}