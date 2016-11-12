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

        private readonly IUrlValidator urlValidator;

        public OpenAuthorizationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<TOptions> options, IOpenAuthorizationHandlerFactory openAuthorizationHandlerFactory, IUrlValidator urlValidator)
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

            if (urlValidator == null)
            {
                throw new ArgumentNullException(nameof(urlValidator));
            }

            this.Options = options.Value;
            this.Logger = loggerFactory.CreateLogger(this.GetType().FullName);
            this.next = next;
            this.openAuthorizationHandlerFactory = openAuthorizationHandlerFactory;
            this.urlValidator = urlValidator;

            this.ValidateOptions();
        }

        private ILogger Logger { get; set; }

        private TOptions Options { get; set; }

        public async Task Invoke(HttpContext context)
        {
            if (!this.urlValidator.IsRelevantRequest(context, this.Options))
            {
                await this.next(context);
                return;
            }

            var handler = this.CreateHandler();

            try
            {
                if (this.urlValidator.IsAuthorizationRequest(context, this.Options))
                {
                    await handler.HandleAuthorizationRequest(context);
                }

                if (this.urlValidator.IsAuthorizationCallbackRequest(context, this.Options))
                {
                    await handler.HandleAuthorizeCallback(context);
                }
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
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.AccessTokenEndpointUri), this.Options.ServiceProviderName));
            }

            if (string.IsNullOrWhiteSpace(this.Options.AuthorizeEndpointUri))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.AuthorizeEndpointUri), this.Options.ServiceProviderName));
            }

            if (string.IsNullOrWhiteSpace(this.Options.ApplicationKey))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.ApplicationKey), this.Options.ServiceProviderName));
            }

            if (string.IsNullOrWhiteSpace(this.Options.ApplicationSecret))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.ApplicationSecret), this.Options.ServiceProviderName));
            }

            if (string.IsNullOrWhiteSpace(this.Options.ServiceProviderName))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(this.Options.ServiceProviderName), this.Options.ServiceProviderName));
            }
        }
    }
}