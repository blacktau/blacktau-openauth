namespace Blacktau.OpenAuth.Client.TestHarness.Tumblr
{
    using Microsoft.Extensions.Configuration;

    public class TumblrProvider
    {
        private readonly string tumblrAccessToken;

        private readonly string tumblrAccessTokenSecret;

        private readonly string tumblrApplicationKey;

        private readonly string tumblrApplicationSecret;

        public TumblrProvider(IConfiguration configuration)
        {
            this.tumblrAccessToken = configuration["Authorization:Tumblr:AccessToken"];
            this.tumblrAccessTokenSecret = configuration["Authorization:Tumblr:AccessTokenSecret"];
            this.tumblrApplicationKey = configuration["Authorization:Tumblr:ApplicationKey"];
            this.tumblrApplicationSecret = configuration["Authorization:Tumblr:ApplicationSecret"];
        }

        public ApplicationCredentials CreateTumblrApplicationCredentials()
        {
            return new ApplicationCredentials { ApplicationKey = this.tumblrApplicationKey, ApplicationSecret = this.tumblrApplicationSecret };
        }

        public AuthorizationInformation CreateTumblrAuthorizationInformation()
        {
            return new AuthorizationInformation { AccessToken = this.tumblrAccessToken, AccessTokenSecret = this.tumblrAccessTokenSecret };
        }
    }
}