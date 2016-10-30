namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationResourceProviderRegistery : IOpenAuthorizationResourceProviderRegistery
    {
        private readonly List<OpenAuthorizationOptions> configuredOptions;

        private readonly ILogger<OpenAuthorizationResourceProviderRegistery> logger;

        public OpenAuthorizationResourceProviderRegistery(ILogger<OpenAuthorizationResourceProviderRegistery> logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.logger = logger;
            this.configuredOptions = new List<OpenAuthorizationOptions>();
        }

        public IEnumerable<OAuthResourceProviderDescription> GetConfiguredResourceProviders()
        {
            if (this.configuredOptions.Count == 0)
            {
                return new OAuthResourceProviderDescription[0];
            }

            return this.configuredOptions.Select(o => o.Description);
        }

        public void Register(OpenAuthorizationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (this.IsProviderAlreadyConfigured(options))
            {
                this.logger.LogWarning("Duplicate ResourceProvider Configured {0}. Using last configured", options.DisplayName);
                this.DeregisterProvider(options.DisplayName);
            }

            this.configuredOptions.Add(options);
        }

        private void DeregisterProvider(string providerName)
        {
            var oldOption = this.configuredOptions.FirstOrDefault(o => o.DisplayName == providerName);
            this.configuredOptions.Remove(oldOption);
        }

        private bool IsProviderAlreadyConfigured(OpenAuthorizationOptions options)
        {
            return this.configuredOptions.Any(o => o.DisplayName == options.DisplayName);
        }
    }
}