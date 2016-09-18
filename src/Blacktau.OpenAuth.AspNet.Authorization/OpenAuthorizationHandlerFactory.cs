namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Globalization;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;

    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationHandlerFactory : IOpenAuthorizationHandlerFactory
    {
        private readonly ILoggerFactory loggerFactory;

        private readonly ILogger<OpenAuthorizationHandlerFactory> logger;

        public OpenAuthorizationHandlerFactory(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;

            this.logger = this.loggerFactory.CreateLogger<OpenAuthorizationHandlerFactory>();
        }

        public IOpenAuthorizationHandler CreateHandler(OpenAuthorizationOptions options)
        {
            if (options.OpenAuthVersion == OpenAuthVersion.OneA)
            {
                this.logger.LogInformation("returning handler for OAuth Version 1.0A");
                return new OpenAuthorizationVersionOneAHandler(this.loggerFactory, options);
            }
            
            throw new Exception(string.Format(CultureInfo.CurrentCulture, OpenAuth.Resources.Exception_Invalid, options.OpenAuthVersion));
        }
    }
}