namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationResourceProviderRegistery : IOpenAuthorizationResourceProviderRegistery
    {
        private readonly List<IOpenAuthorizationOptions> configuredOptions;

        private readonly ILogger<OpenAuthorizationResourceProviderRegistery> logger;

        private readonly IUrlValidator urlValidator;

        public OpenAuthorizationResourceProviderRegistery(ILogger<OpenAuthorizationResourceProviderRegistery> logger, IUrlValidator urlValidator)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            if (urlValidator == null)
            {
                throw new ArgumentNullException(nameof(urlValidator));
            }
            
            this.logger = logger;
            this.urlValidator = urlValidator;
            this.configuredOptions = new List<IOpenAuthorizationOptions>();
        }

        public IEnumerable<OAuthResourceProviderDescription> GetConfiguredResourceProviders()
        {
            if (this.configuredOptions.Count == 0)
            {
                return new OAuthResourceProviderDescription[0];
            }

            return this.configuredOptions.Select(o => o.Description);
        }

        public void Register(IOpenAuthorizationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (this.IsProviderAlreadyConfigured(options))
            {
                this.logger.LogWarning("Duplicate ResourceProvider Configured {0}. Using last configured", options.Description.ServiceProviderName);
                this.DeregisterProvider(options.ServiceProviderName);
            }

            this.configuredOptions.Add(options);
            options.Description.ActivationPath = this.urlValidator.GetActivationPath(options);
        }

        private void DeregisterProvider(string providerName)
        {
            var oldOption = this.configuredOptions.FirstOrDefault(o => o.ServiceProviderName == providerName);
            this.configuredOptions.Remove(oldOption);
        }

        private bool IsProviderAlreadyConfigured(IOpenAuthorizationOptions options)
        {
            return this.configuredOptions.Any(o => o.ServiceProviderName == options.ServiceProviderName);
        }
    }
}