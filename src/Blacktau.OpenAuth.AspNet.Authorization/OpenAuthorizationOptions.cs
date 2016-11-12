namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using Microsoft.AspNetCore.Http;

    public abstract class OpenAuthorizationOptions : IOpenAuthorizationOptions
    {
        protected OpenAuthorizationOptions()
        {
            this.Description = new OAuthResourceProviderDescription { ServiceProviderName = this.ServiceProviderName, OpenAuthProtocolVersion = this.GetOpenAuthVersionName() };
        }

        public abstract string RequestStateStorageKey { get; }

        public abstract string AccessTokenEndpointUri { get; }

        public string ApplicationKey { get; set; }

        public string ApplicationSecret { get; set; }

        public abstract string AuthorizeEndpointUri { get; }

        public OAuthResourceProviderDescription Description { get; }

        public Func<Exception, HttpContext, Task> FailureHandler { get; set; } = (exception, context) => Task.CompletedTask;

        public abstract OpenAuthVersion OpenAuthVersion { get; }

        public abstract string ServiceProviderName { get; }

        public Func<IAuthorizationInformation, HttpContext, Task> SuccessHandler { get; set; } = (exception, context) => Task.CompletedTask;

        public abstract IAuthorizationInformation ExtractAuthorizationInformation(IDictionary<string, string> parameters);

        protected string GetAuthorizationFieldValue(IDictionary<string, string> parameters, string fieldName)
        {
            return parameters.ContainsKey(fieldName) ? parameters[fieldName] : null;
        }

        private string GetOpenAuthVersionName()
        {
            return this.OpenAuthVersion == OpenAuthVersion.OneA ? "1.0a" : "2.0";
        }
    }
}