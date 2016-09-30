namespace Blacktau.OpenAuth.Client.TestHarness.Twitter
{
    public class TwitterProvider
    {
        private const string TwitterApplicationKey = "lBdRWTOhTX12lH92QrNvPqLpE";

        private const string TwitterApplicationSecret = "7OOsw0qgmGjadqOmeF52pmEGYrtJrv9rpel2PCvaoCFuh8U9nW";

        private const string TwitterAccessToken = "40424119-I6BKoDZOYL9Vl0nPvNHt6LHsydMG776GfDDXLUdWk";

        private const string TwitterAccessTokenSecret = "W4NMkwLq2pm0S07Grd6Yn3sqS7MDha0hkGPAUSw3uY86p";

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