namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;

    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationOneAHandlerFactory : IOpenAuthorizationOneAHandlerFactory
    {
        private readonly IVersionOneACallbackHandler versionOneACallbackHandler;

        private readonly ILogger<OpenAuthorizationOneAHandlerFactory> logger;

        private readonly ILoggerFactory loggerFactory;

        private readonly IVersionOneAAuthorizationRequestor versionOneAAuthorizationRequestor;

        public OpenAuthorizationOneAHandlerFactory(ILoggerFactory loggerFactory, IVersionOneACallbackHandler versionOneACallbackHandler, IVersionOneAAuthorizationRequestor versionOneAAuthorizationRequestor)
        {
            this.loggerFactory = loggerFactory;
            this.versionOneACallbackHandler = versionOneACallbackHandler;
            this.versionOneAAuthorizationRequestor = versionOneAAuthorizationRequestor;
            this.logger = loggerFactory.CreateLogger<OpenAuthorizationOneAHandlerFactory>();
        }

        public IOpenAuthorizationHandler CreateHandler(IOpenAuthorizationOptions options)
        {
            this.logger.LogInformation("returning handler for OAuth Version 1.0A");
            var versionOneOptions = options as IVersionOneAOpenAuthorizationOptions;
            return new OpenAuthorizationVersionOneAHandler(this.loggerFactory, this.versionOneAAuthorizationRequestor, this.versionOneACallbackHandler, versionOneOptions);
        }
    }
}