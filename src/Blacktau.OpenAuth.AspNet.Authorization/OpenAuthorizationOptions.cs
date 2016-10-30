namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;

    using Microsoft.AspNetCore.Http;

    public class OpenAuthorizationOptions : IOpenAuthorizationOptions
    {
        private string authorisePath;

        private string callBackUrl;

        private string pathBase;

        private string serviceProviderName;

        private OpenAuthVersion openAuthVersion;

        public string AccessTokenEndpointUri { get; set; }

        public string AuthorizeEndpointUri { get; set; }

        public OAuthResourceProviderDescription Description { get; set; } = new OAuthResourceProviderDescription();

        public string DisplayName
        {
            get
            {
                return this.Description.DisplayName;
            }

            set
            {
                this.Description.DisplayName = value;
            }
        }

        public Func<Exception, HttpContext, Task> FailureHandler { get; set; } = (exception, context) => Task.CompletedTask;

        public OpenAuthVersion OpenAuthVersion
        {
            get
            {
                return this.openAuthVersion;
            }

            set
            {
                this.openAuthVersion = value;
                this.UpdateVersionName();
            }
        }

        private void UpdateVersionName()
        {
            this.Description.OpenAuthProtocolVersion = this.openAuthVersion == OpenAuthVersion.OneA ? "1.0a" : "2.0";
        }

        public string RequestTokenEndpointUri { get; set; }

        public string ServiceProviderName
        {
            get
            {
                return this.serviceProviderName;
            }

            set
            {
                this.serviceProviderName = value;
                this.Description.ServiceProviderName = value;
                this.UpdatePaths();
            }
        }

        internal IApplicationCredentials ApplicationCredentials { get; set; } = new ApplicationCredentials();

        protected string ApplicationKey
        {
            get
            {
                return this.ApplicationCredentials.ApplicationKey;
            }

            set
            {
                this.ApplicationCredentials.ApplicationKey = value;
            }
        }

        protected string ApplicationSecret
        {
            get
            {
                return this.ApplicationCredentials.ApplicationSecret;
            }

            set
            {
                this.ApplicationCredentials.ApplicationSecret = value;
            }
        }

        public string GetCallBackUrl(HttpContext context)
        {
            var currentUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}{this.callBackUrl}";

            return currentUrl;
        }

        internal bool IsAuthoriseRequest(HttpContext context)
        {
            return context.Request.Path.Value.Contains(this.authorisePath);
        }

        internal bool IsCallbackRequest(HttpContext context)
        {
            return context.Request.Path.Value.Contains(this.callBackUrl);
        }

        internal bool IsRelevantRequest(HttpContext context)
        {
            return context.Request.Path.Value.Contains(this.pathBase);
        }

        private void UpdatePaths()
        {
            this.pathBase = string.Format("/OpenAuthorization/{0}", this.serviceProviderName);
            this.authorisePath = string.Format("{0}/AuthorizeRequest", this.pathBase);
            this.callBackUrl = string.Format("{0}/AuthorizeResponse", this.pathBase);

            this.Description.ActivationPath = this.authorisePath;
        }
    }
}