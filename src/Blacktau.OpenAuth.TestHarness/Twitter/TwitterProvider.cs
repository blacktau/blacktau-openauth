namespace Blacktau.OpenAuth.TestHarness.Twitter
{
    public class TwitterProvider
    {
        private const string TwitterApplicationKey = "<Twitter Application/Consumer Key Goes Here>";

        private const string TwitterApplicationSecret = "<Twitter Application/Consumer Secret Goes Here>";

        private const string TwitterAccessToken = "<Twitter Access Token Goes Here>";

        private const string TwitterAccessTokenSecret = "<Twitter Access Token Secret Goes Here>";

        public static ApplicationCredentials CreateTwitterApplicationCredentials()
        {
            return new ApplicationCredentials
            {
                ApplicationKey = TwitterApplicationKey,
                ApplicationSecret = TwitterApplicationSecret
            };
        }

        public static AuthorizationInformation CreateTwitterAuthorizationInformation()
        {
            return new AuthorizationInformation(TwitterAccessToken) { AccessTokenSecret = TwitterAccessTokenSecret };
        }
    }
}