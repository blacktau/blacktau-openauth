namespace Blacktau.OpenAuth.TestHarness.Tumblr
{
    public class TumblrProvider
    {
        private const string TumblrAccessToken = "<Tumblr Access Token Goes Here>";

        private const string TumblrAccessTokenSecret = "<Tumblr Access Token Secret Goes Here>";

        private const string TumblrApplicationKey = "<Tumblr Application/Consumer Key Goes Here>";

        private const string TumblrApplicationSecret = "<Tumblr Application/Consumer Secret Goes Here>";

        public static ApplicationCredentials CreateTumblrApplicationCredentials()
        {
            return new ApplicationCredentials
            {
                ApplicationKey = TumblrApplicationKey,
                ApplicationSecret = TumblrApplicationSecret
            };
        }

        public static AuthorizationInformation CreateTumblrAuthorizationInformation()
        {
            return new AuthorizationInformation(TumblrAccessToken) {AccessTokenSecret = TumblrAccessTokenSecret};
        }
    }
}