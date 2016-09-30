namespace Blacktau.OpenAuth.Client.TestHarness.Twitter.Statuses
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Containers.Basic;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class GetUserTimeline
    {
        public async Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = TwitterProvider.CreateTwitterApplicationCredentials();

            IAuthorizationInformation authorizationInformation = TwitterProvider.CreateTwitterAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory();

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.twitter.com/1.1/statuses/user_timeline.json", HttpMethod.Get, OpenAuthVersion.OneA, applicationCredentials, authorizationInformation);

            openAuthClient.AddQueryParameter("screen_name", "blacktau");
            openAuthClient.AddQueryParameter("count", "2");

            var result = await openAuthClient.Execute();
            return result;
        }

    }
}