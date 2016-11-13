namespace Blacktau.OpenAuth.Client.TestHarness.Facebook
{
    using Microsoft.Extensions.Configuration;

    public class FacebookProvider
    {
        private readonly string facebookAccessToken;

        private readonly string facebookApplicationId;

        private readonly string facebookApplicationSecret;

        public FacebookProvider(IConfiguration configuration)
        {
            this.facebookApplicationId = configuration["Authorization:Facebook:ApplicationId"];
            this.facebookApplicationSecret = configuration["Authorization:Facebook:ApplicationSecret"];
            this.facebookAccessToken = configuration["Authorization:Facebook:AccessToken"];
        }

        public ApplicationCredentials CreateFacebookApplicationCredentials()
        {
            return new ApplicationCredentials { ApplicationKey = this.facebookApplicationId, ApplicationSecret = this.facebookApplicationSecret };
        }

        public AuthorizationInformation CreateFacebookAuthorizationInformation()
        {
            return new AuthorizationInformation { AccessToken = this.facebookAccessToken };
        }
    }
}