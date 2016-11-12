namespace Blacktau.OpenAuth.AspNet.Authorization.VersionTwo
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo;

    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationTwoHandlerFactory : IOpenAuthorizationTwoHandlerFactory
    {
        private readonly ILogger<OpenAuthorizationTwoHandlerFactory> logger;

        private readonly ILoggerFactory loggerFactory;

        private readonly IVersionTwoAuthorizationRequestor versionTwoAuthorizationRequestor;

        private readonly IVersionTwoCallbackHandler callbackHandler;

        public OpenAuthorizationTwoHandlerFactory(ILoggerFactory loggerFactory, IVersionTwoAuthorizationRequestor versionTwoAuthorizationRequestor, IVersionTwoCallbackHandler callbackHandler)
        {
            this.loggerFactory = loggerFactory;
            this.versionTwoAuthorizationRequestor = versionTwoAuthorizationRequestor;
            this.callbackHandler = callbackHandler;
            this.logger = loggerFactory.CreateLogger<OpenAuthorizationTwoHandlerFactory>();
        }

        public IOpenAuthorizationHandler CreateHandler(IOpenAuthorizationOptions options)
        {
            this.logger.LogInformation("returning handler for OAuth Version 2.0");
            var versionTwoOptions = options as IVersionTwoOpenAuthorizationOptions;
            return new OpenAuthorizationVersionTwoHandler(this.loggerFactory, this.versionTwoAuthorizationRequestor, this.callbackHandler, versionTwoOptions);
        }
    }
}