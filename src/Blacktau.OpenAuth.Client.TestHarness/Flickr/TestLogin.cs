namespace Blacktau.OpenAuth.Client.TestHarness.Flickr
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Containers.Basic;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class TestLogin
    {
        private readonly FlickrProvider provider;

        public TestLogin(FlickrProvider provider)
        {
            this.provider = provider;
        }

        public async Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = this.provider.CreateApplicationCredentials();

            IAuthorizationInformation authorizationInformation = this.provider.CreateAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory();

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.flickr.com/services/rest", HttpMethod.Post, OpenAuthVersion.OneA, applicationCredentials, authorizationInformation);

            openAuthClient.AddQueryParameter("nojsoncallback", "1");
            openAuthClient.AddQueryParameter("format", "json");
            openAuthClient.AddQueryParameter("method", "flickr.test.login");

            var result = await openAuthClient.Execute();

            return result;
        }
    }
}