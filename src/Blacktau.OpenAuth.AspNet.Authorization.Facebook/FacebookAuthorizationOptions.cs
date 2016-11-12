namespace Blacktau.OpenAuth.AspNet.Authorization.Facebook
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.AspNet.Authorization.VersionTwo;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionTwo;

    public class FacebookAuthorizationOptions : VersionTwoOpenAuthorizationOptions
    {
        public FacebookAuthorizationOptions()
        {
            this.Description.DisplayName = "Facebook";
        }

        public override string AccessTokenEndpointUri => "https://graph.facebook.com/oauth/access_token";

        public string ApplicationId
        {
            get
            {
                return this.ApplicationKey;
            }

            set
            {
                this.ApplicationKey = value;
            }
        }

        public override string AuthorizeEndpointUri => "https://www.facebook.com/dialog/oauth";

        public override string RequestStateStorageKey => "FacebookStateStorageKey";

        public override List<string> Scope => new List<string>();

        public override string ServiceProviderName => "Facebook";

        public override IAuthorizationInformation ExtractAuthorizationInformation(IDictionary<string, string> parameters)
        {
            var authorizationInformation = base.ExtractAuthorizationInformation(parameters);
            authorizationInformation.Expires = this.GetExpiry(parameters, AuthorizationFieldNames.Expires);
            return authorizationInformation;
        }
    }
}