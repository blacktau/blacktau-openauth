namespace Blacktau.OpenAuth.Client.TestHarness.Twitter
{
    using Microsoft.Extensions.Configuration;

    public class TwitterProvider
    {
        private readonly string twitterAccessToken;

        private readonly string twitterAccessTokenSecret;

        private readonly string twitterApplicationKey;

        private readonly string twitterApplicationSecret;

        public TwitterProvider(IConfiguration configurationRoot)
        {
            this.twitterApplicationKey = configurationRoot["Authorization:Twitter:ApplicationKey"];
            this.twitterApplicationSecret = configurationRoot["Authorization:Twitter:ApplicationSecret"];
            this.twitterAccessToken = configurationRoot["Authorization:Twitter:AccessToken"];
            this.twitterAccessTokenSecret = configurationRoot["Authorization:Twitter:AccessTokenSecret"];
        }

        public ApplicationCredentials CreateTwitterApplicationCredentials()
        {
            return new ApplicationCredentials { ApplicationKey = this.twitterApplicationKey, ApplicationSecret = this.twitterApplicationSecret };
        }

        public AuthorizationInformation CreateTwitterAuthorizationInformation()
        {
            return new AuthorizationInformation { AccessToken = this.twitterAccessToken, AccessTokenSecret = this.twitterAccessTokenSecret };
        }
    }
}