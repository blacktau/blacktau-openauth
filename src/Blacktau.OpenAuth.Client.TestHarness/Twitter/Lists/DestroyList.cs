namespace Blacktau.OpenAuth.Client.TestHarness.Twitter.Lists
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Containers.Basic;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class DestroyList
    {
        private readonly TwitterProvider twitterProvider;

        public DestroyList(TwitterProvider twitterProvider)
        {
            this.twitterProvider = twitterProvider;
        }

        public async Task<string> Execute(string id)
        {
            IApplicationCredentials applicationCredentials = this.twitterProvider.CreateTwitterApplicationCredentials();

            IAuthorizationInformation authorizationInformation = this.twitterProvider.CreateTwitterAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory();

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.twitter.com/1.1/lists/destroy.json", HttpMethod.Post, OpenAuthVersion.OneA, applicationCredentials, authorizationInformation);

            openAuthClient.AddQueryParameter("list_id", id);

            var result = await openAuthClient.Execute();
            return result;
        }
    }
}