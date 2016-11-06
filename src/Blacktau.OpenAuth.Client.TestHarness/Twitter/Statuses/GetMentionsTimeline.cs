namespace Blacktau.OpenAuth.Client.TestHarness.Twitter.Statuses
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Containers.Basic;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class GetMentionsTimeline
    {
        private readonly TwitterProvider twitterProvider;

        public GetMentionsTimeline(TwitterProvider twitterProvider)
        {
            this.twitterProvider = twitterProvider;
        }

        public async Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = this.twitterProvider.CreateTwitterApplicationCredentials();

            IAuthorizationInformation authorizationInformation = this.twitterProvider.CreateTwitterAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory();

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.twitter.com/1.1/statuses/mentions_timeline.json", HttpMethod.Get, OpenAuthVersion.OneA, applicationCredentials, authorizationInformation);

            // openAuthClient.AddQueryParameter("screen_name", "blacktau");
            openAuthClient.AddQueryParameter("count", "2");

            var result = await openAuthClient.Execute();
            return result;
        }
    }
}