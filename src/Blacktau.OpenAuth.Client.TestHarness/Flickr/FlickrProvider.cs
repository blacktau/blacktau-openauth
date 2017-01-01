namespace Blacktau.OpenAuth.Client.TestHarness.Flickr
{
    using Microsoft.Extensions.Configuration;

    public class FlickrProvider
    {
        private readonly string accessToken;

        private readonly string accessTokenSecret;

        private readonly string applicationKey;

        private readonly string applicationSecret;

        public FlickrProvider(IConfigurationRoot configurationRoot)
        {
            this.applicationKey = configurationRoot["Authorization:Flickr:Key"];
            this.applicationSecret = configurationRoot["Authorization:Flickr:Secret"];
            this.accessToken = configurationRoot["Authorization:Flickr:AccessToken"];
            this.accessTokenSecret = configurationRoot["Authorization:Flickr:AccessTokenSecret"];
        }

        public ApplicationCredentials CreateApplicationCredentials()
        {
            return new ApplicationCredentials { ApplicationKey = this.applicationKey, ApplicationSecret = this.applicationSecret };
        }

        public AuthorizationInformation CreateAuthorizationInformation()
        {
            return new AuthorizationInformation(this.accessToken) { AccessTokenSecret = this.accessTokenSecret };
        }
    }
}