namespace Blacktau.OpenAuth.TestHarness.Facebook
{
    public class FacebookProvider
    {
        private const string FacebookApplicationId = "<Facebook Application Id Goes Here>";

        private const string FacebookApplicationSecret = "<Facebook Application Secret Goes Here>";

        private const string FacebookAccessToken = "<Facebook Access Token for User Goes Here>";

        public static ApplicationCredentials CreateFacebookApplicationCredentials()
        {
            return new ApplicationCredentials
            {
                ApplicationKey = FacebookApplicationId,
                ApplicationSecret = FacebookApplicationSecret
            };
        }

        public static AuthorizationInformation CreateFacebookAuthorizationInformation()
        {
            return new AuthorizationInformation(FacebookAccessToken);
        }
    }
}