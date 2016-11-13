namespace Blacktau.OpenAuth.AspNet.Authorization.Twitter
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    public sealed class TwitterAuthorizationOptions : VersionOneOpenAuthorizationOptions
    {
        public TwitterAuthorizationOptions()
        {
            this.Description.DisplayName = "Twitter";
        }

        public override string RequestStateStorageKey => "TwitterStateKey";

        public override string AccessTokenEndpointUri => "https://api.twitter.com/oauth/access_token";

        public override string AuthorizeEndpointUri => "https://api.twitter.com/oauth/authorize";

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

        public override string RequestTokenEndpointUri => "https://api.twitter.com/oauth/request_token";

        public override string ServiceProviderName => "Twitter";

        public override IAuthorizationInformation ExtractAuthorizationInformation(IDictionary<string, string> parameters)
        {
            var authorizationInformation = new TwitterAuthorizationInformation();

            authorizationInformation.AccessToken = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.Token);
            authorizationInformation.AccessTokenSecret = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.TokenSecret);

            authorizationInformation.UserId = this.GetAuthorizationFieldValue(parameters, "user_id");
            authorizationInformation.ScreenName = this.GetAuthorizationFieldValue(parameters, "screen_name");

            return authorizationInformation;
        }
    }
}