namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;

    public class OpenAuthorizationVersionOneAHandler : IOpenAuthorizationHandler
    {
        private readonly ILogger logger;

        private readonly OpenAuthorizationOptions options;

        private readonly IAuthorizationRequestor authorizationRequestor;

        private readonly ICallbackHandler callbackHandler;

        public OpenAuthorizationVersionOneAHandler(ILoggerFactory loggerFactory, IAuthorizationRequestor authorizationRequestor, ICallbackHandler callbackHandler, OpenAuthorizationOptions options)
        {
            this.authorizationRequestor = authorizationRequestor;
            this.callbackHandler = callbackHandler;
            this.options = options;
            this.logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        public async Task HandleRequest(HttpContext context)
        {
            if (this.options.IsAuthoriseRequest(context))
            {
                await this.authorizationRequestor.RequestAuthorization(context, this.options);
                return;
            }

            if (this.options.IsCallbackRequest(context))
            {
                await this.callbackHandler.HandleCallBack(context, this.options);
            }
        }

        public Task TeardownAsync()
        {
            return Task.CompletedTask;
        }
    }
}