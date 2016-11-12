namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationVersionOneAHandler : IOpenAuthorizationHandler
    {
        private readonly IVersionOneACallbackHandler versionOneACallbackHandler;

        private readonly ILogger logger;

        private readonly IVersionOneAOpenAuthorizationOptions options;

        private readonly IVersionOneAAuthorizationRequestor versionOneAAuthorizationRequestor;

        public OpenAuthorizationVersionOneAHandler(ILoggerFactory loggerFactory, IVersionOneAAuthorizationRequestor versionOneAAuthorizationRequestor, IVersionOneACallbackHandler versionOneACallbackHandler, IVersionOneAOpenAuthorizationOptions options)
        {
            this.versionOneAAuthorizationRequestor = versionOneAAuthorizationRequestor;
            this.versionOneACallbackHandler = versionOneACallbackHandler;
            this.options = options;
            this.logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        public Task HandleAuthorizationRequest(HttpContext context)
        {
            return this.versionOneAAuthorizationRequestor.RequestAuthorization(context, this.options);
        }

        public Task HandleAuthorizeCallback(HttpContext context)
        {
            return this.versionOneACallbackHandler.HandleCallBack(context, this.options);
        }

        public Task TeardownAsync()
        {
            return Task.CompletedTask;
        }
    }
}