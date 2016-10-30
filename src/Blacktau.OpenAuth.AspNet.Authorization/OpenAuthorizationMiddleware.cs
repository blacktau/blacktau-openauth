namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class OpenAuthorizationMiddleware<TOptions>
        where TOptions : OpenAuthorizationOptions, new()
    {
        private readonly RequestDelegate next;

        private readonly IOpenAuthorizationHandlerFactory openAuthorizationHandlerFactory;

        public OpenAuthorizationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<TOptions> options, IOpenAuthorizationHandlerFactory openAuthorizationHandlerFactory)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (openAuthorizationHandlerFactory == null)
            {
                throw new ArgumentNullException(nameof(openAuthorizationHandlerFactory));
            }

            this.Options = options.Value;
            this.Logger = loggerFactory.CreateLogger(this.GetType().FullName);
            this.next = next;
            this.openAuthorizationHandlerFactory = openAuthorizationHandlerFactory;
            

            this.ValidateOptions();
        }

        protected ILogger Logger { get; set; }

        protected TOptions Options { get; private set; }

        public async Task Invoke(HttpContext context)
        {
            
            if (!this.Options.IsRelevantRequest(context))
            {
                await this.next(context);
                return;
            }

            var handler = this.CreateHandler();

            try
            {
                await handler.HandleRequest(context);
            }
            finally
            {
                try
                {
                    await handler.TeardownAsync();
                }
                catch (Exception)
                {
                    // Don't mask the original exception, if any
                }
            }
        }

        private IOpenAuthorizationHandler CreateHandler()
        {
            return this.openAuthorizationHandlerFactory.CreateHandler(this.Options);
        }

        private void ValidateOptions()
        {
            if (string.IsNullOrWhiteSpace(this.Options.AccessTokenEndpointUri))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.AccessTokenEndpointUri)));
            }

            if (string.IsNullOrWhiteSpace(this.Options.AuthorizeEndpointUri))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.AuthorizeEndpointUri)));
            }

            if (string.IsNullOrWhiteSpace(this.Options.RequestTokenEndpointUri))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.RequestTokenEndpointUri)));
            }

            if (this.Options.ApplicationCredentials == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.ApplicationCredentials)));
            }

            if (string.IsNullOrWhiteSpace(this.Options.ApplicationCredentials.ApplicationKey))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.ApplicationCredentials.ApplicationKey)));
            }

            if (string.IsNullOrWhiteSpace(this.Options.ApplicationCredentials.ApplicationSecret))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.ApplicationCredentials.ApplicationSecret)));
            }

            if (string.IsNullOrWhiteSpace(this.Options.ServiceProviderName))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.ServiceProviderName)));
            }
        }
    }
}