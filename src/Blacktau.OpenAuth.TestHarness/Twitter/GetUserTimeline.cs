namespace Blacktau.OpenAuth.TestHarness.Twitter
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Basic;
    using Blacktau.OpenAuth.Interfaces;

    public class GetUserTimeline
    {
        public async Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = TwitterProvider.CreateTwitterApplicationCredentials();

            IAuthorizationInformation authorizationInformation = TwitterProvider.CreateTwitterAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory(applicationCredentials, authorizationInformation);

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.twitter.com/1.1/statuses/user_timeline.json", HttpMethod.Get, OpenAuthVersion.OneA);

            openAuthClient.AddQueryParameter("screen_name", "blacktau");
            openAuthClient.AddQueryParameter("count", "2");

            var result = await openAuthClient.Execute();
            return result;
        }

    }
}