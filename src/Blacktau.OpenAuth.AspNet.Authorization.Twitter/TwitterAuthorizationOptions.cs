namespace Blacktau.OpenAuth.AspNet.Authorization.Twitter
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    public class TwitterAuthorizationOptions : OpenAuthorizationOptions
    {
        public TwitterAuthorizationOptions()
        {
            this.Description = new OAuthResourceProviderDescription { DisplayName = "Twitter" };
            this.ServiceProviderName = "Twitter";

            this.AccessTokenEndpointUri = "https://api.twitter.com/oauth/access_token";
            this.AuthorizeEndpointUri = "https://api.twitter.com/oauth/authorize";
            this.RequestTokenEndpointUri = "https://api.twitter.com/oauth/request_token";
        }

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

        protected override IAuthorizationInformation GetAuthorizationInformation(IDictionary<string, string> parameters)
        {
            var authorizationInformation = new TwitterAuthorizationInformation(null);

            authorizationInformation.AccessToken = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.Token);
            authorizationInformation.AccessTokenSecret = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.TokenSecret);

            authorizationInformation.UserId = this.GetAuthorizationFieldValue(parameters, "user_id");
            authorizationInformation.ScreenName = this.GetAuthorizationFieldValue(parameters, "screen_name");

            return authorizationInformation;
        }
    }
}