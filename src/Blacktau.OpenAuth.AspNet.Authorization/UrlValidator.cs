namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.AspNetCore.Http;

    internal class UrlValidator : IUrlValidator
    {
        private const string AuthorizeRequestPathTemplate = "/OpenAuthorization/{0}/AuthorizeRequest/";

        private const string CallbackRequestPathTemplate = "/OpenAuthorization/{0}/AuthorizeResponse/";

        public bool IsAuthorizationCallbackRequest(HttpContext context, IOpenAuthorizationOptions options)
        {
            var callBackPath = GetCallbackPath(options);
            return context.Request.Path.Equals(callBackPath);
        }

        public string GetCallbackUrl(HttpContext context, IOpenAuthorizationOptions options)
        {
            var callbackUrl = GetCallbackPath(options);
            return $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}{callbackUrl}";
        }
        
        public bool IsAuthorizationRequest(HttpContext context, IOpenAuthorizationOptions options)
        {
            var authorizationPath = GetAuthorizationPath(options);
            return context.Request.Path.Equals(authorizationPath);
        }

        public bool IsRelevantRequest(HttpContext context, IOpenAuthorizationOptions options)
        {
            return this.IsAuthorizationRequest(context, options) || this.IsAuthorizationCallbackRequest(context, options);
        }

        public string GetActivationPath(IOpenAuthorizationOptions options)
        {
            return GetAuthorizationPath(options);
        }

        private static string GetCallbackPath(IOpenAuthorizationOptions options)
        {
            return string.Format(CallbackRequestPathTemplate, options.ServiceProviderName);
        }

        private static string GetAuthorizationPath(IOpenAuthorizationOptions options)
        {
            return string.Format(AuthorizeRequestPathTemplate, options.ServiceProviderName);
        }
    }
}