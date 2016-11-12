namespace Blacktau.OpenAuth.AspNet.Authorization.VersionTwo
{
    using System;
    using System.Collections.Generic;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo;
    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class VersionTwoAuthorizationRequestor : IVersionTwoAuthorizationRequestor
    {
        private readonly IVersionTwoAuthorizationParametersGenerator authorizationParametersGenerator;

        private readonly IStateStorageManager storageManager;

        private readonly IUrlValidator urlValidator;

        private ILogger<VersionTwoAuthorizationRequestor> logger;

        public VersionTwoAuthorizationRequestor(IStateStorageManager storageManager, ILoggerFactory loggerFactory, IUrlValidator urlValidator, IVersionTwoAuthorizationParametersGenerator authorizationParametersGenerator)
        {
            this.storageManager = storageManager;
            this.urlValidator = urlValidator;
            this.authorizationParametersGenerator = authorizationParametersGenerator;
            this.logger = loggerFactory.CreateLogger<VersionTwoAuthorizationRequestor>();
        }

        public void RequestAuthorization(HttpContext context, IVersionTwoOpenAuthorizationOptions options)
        {
            var queryString = this.CreateQueryString(context, options);
            var uriBuilder = new UriBuilder(options.AuthorizeEndpointUri) { Query = queryString };
            context.Response.Redirect(uriBuilder.Uri.AbsoluteUri);
        }

        private static string GetScopeString(IVersionTwoOpenAuthorizationOptions options)
        {
            var scope = string.Join(" ", options.Scope);
            return scope;
        }

        private string CreateQueryString(HttpContext context, IVersionTwoOpenAuthorizationOptions options)
        {
            var requestParameters = this.GetRequestParameters(context, options);
            return requestParameters.ToQueryString();
        }

        private string CreateState(HttpContext context, IVersionTwoOpenAuthorizationOptions options)
        {
            var state = this.GenerateStateValue();
            this.storageManager.Store(context, options.RequestStateStorageKey, state);
            return state;
        }

        private string GenerateStateValue()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty);
        }

        private IDictionary<string, string> GetRequestParameters(HttpContext context, IVersionTwoOpenAuthorizationOptions options)
        {
            var applicationCredentials = options.GetApplicationCredentials();
            var callBackUrl = this.urlValidator.GetCallbackUrl(context, options);
            var scope = GetScopeString(options);
            var state = this.CreateState(context, options);

            return this.authorizationParametersGenerator.GetAuthorizationRequestParameters(applicationCredentials, callBackUrl, scope, state);
        }
    }
}