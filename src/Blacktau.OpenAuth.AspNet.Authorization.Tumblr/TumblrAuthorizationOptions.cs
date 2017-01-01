namespace Blacktau.OpenAuth.AspNet.Authorization.Tumblr
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    public sealed class TumblrAuthorizationOptions : VersionOneOpenAuthorizationOptions
    {
        public TumblrAuthorizationOptions()
        {
            this.Description.DisplayName = "Tumblr";
        }

        public override string RequestStateStorageKey => "TumblrStateKey";

        public override string AccessTokenEndpointUri => "https://www.tumblr.com/oauth/access_token";

        public override string AuthorizeEndpointUri => "https://www.tumblr.com/oauth/authorize";

        public string ConsumerKey
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

        public string ConsumerSecret
        {
            get
            {
                return this.ApplicationSecret;
            }

            set
            {
                this.ApplicationSecret = value;
            }
        }

        public override string RequestTokenEndpointUri => "https://www.tumblr.com/oauth/request_token";

        public override string ServiceProviderName => "Tumblr";

        public override IAuthorizationInformation ExtractAuthorizationInformation(IDictionary<string, string> parameters)
        {
            var authorizationInformation = new TumblrAuthorizationInformation(null);

            authorizationInformation.AccessToken = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.Token);
            authorizationInformation.AccessTokenSecret = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.TokenSecret);

            authorizationInformation.UserId = this.GetAuthorizationFieldValue(parameters, "user_id");
            authorizationInformation.ScreenName = this.GetAuthorizationFieldValue(parameters, "screen_name");

            return authorizationInformation;
        }
    }
}