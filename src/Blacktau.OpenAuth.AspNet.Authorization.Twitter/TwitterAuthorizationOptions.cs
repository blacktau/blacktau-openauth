namespace Blacktau.OpenAuth.AspNet.Authorization.Twitter
{
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
    }
}