namespace Blacktau.OpenAuth.Client.TestHarness.Flickr.Photosets
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Containers.Basic;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class GetList
    {
        private readonly FlickrProvider provider;

        public GetList(FlickrProvider provider)
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
            openAuthClient.AddQueryParameter("method", "flickr.photosets.getList");

            var result = await openAuthClient.Execute();

            return result;
        }
    }
}