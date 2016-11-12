namespace Blacktau.OpenAuth.AspNet.Authorization.VersionTwo
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationVersionTwoHandler : IOpenAuthorizationHandler
    {
        private readonly IVersionTwoCallbackHandler versionTwoCallbackHandler;

        private readonly ILogger logger;

        private readonly IVersionTwoOpenAuthorizationOptions options;

        private readonly IVersionTwoAuthorizationRequestor versionTwoAuthorizationRequestor;

        public OpenAuthorizationVersionTwoHandler(ILoggerFactory loggerFactory, IVersionTwoAuthorizationRequestor versionTwoAuthorizationRequestor, IVersionTwoCallbackHandler versionTwoCallbackHandler, IVersionTwoOpenAuthorizationOptions options)
        {
            this.versionTwoAuthorizationRequestor = versionTwoAuthorizationRequestor;
            this.versionTwoCallbackHandler = versionTwoCallbackHandler;
            this.options = options;
            this.logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        public Task HandleAuthorizationRequest(HttpContext context)
        {
            this.versionTwoAuthorizationRequestor.RequestAuthorization(context, this.options);
            return Task.CompletedTask;
        }

        public Task HandleAuthorizeCallback(HttpContext context)
        {
            return this.versionTwoCallbackHandler.HandleCallBack(context, this.options);
        }

        public Task TeardownAsync()
        {
            return Task.CompletedTask;
        }
    }
}