namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationVersionOneAHandler : IOpenAuthorizationHandler
    {
        private ILogger logger;

        private OpenAuthorizationOptions options;

        public OpenAuthorizationVersionOneAHandler(ILoggerFactory loggerFactory, OpenAuthorizationOptions options)
        {
            this.options = options;
            this.logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        private void CreateCallbackUrl(HttpContext context)
        {

        }

        public Task InitializeAsync(HttpContext context, ILogger logger)
        {
            this.logger = logger;
            return Task.FromResult(0);
        }

        public Task HandleRequest(HttpContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}