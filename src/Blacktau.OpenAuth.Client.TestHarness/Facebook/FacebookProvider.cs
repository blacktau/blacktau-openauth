namespace Blacktau.OpenAuth.Client.TestHarness.Facebook
{
    using Microsoft.Extensions.Configuration;

    public class FacebookProvider
    {
        private readonly string facebookApplicationId;

        private readonly string facebookApplicationSecret;

        private readonly string facebookAccessToken;

        public FacebookProvider(IConfigurationRoot configuration)
        {
            this.facebookApplicationId = configuration["Authorization:Facebook:ApplicationId"];
            this.facebookApplicationSecret = configuration["Authorization:Facebook:ApplicationSecret"];
            this.facebookAccessToken = configuration["Authorization:Facebook:AccessToken"];
        }

        public ApplicationCredentials CreateFacebookApplicationCredentials()
        {
            return new ApplicationCredentials
            {
                ApplicationKey = this.facebookApplicationId,
                ApplicationSecret = this.facebookApplicationSecret
            };
        }

        public AuthorizationInformation CreateFacebookAuthorizationInformation()
        {
            return new AuthorizationInformation(this.facebookAccessToken);
        }
    }
}