namespace Blacktau.OpenAuth.TestHarness.Tumblr
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Basic;
    using Blacktau.OpenAuth.Interfaces;

    public class AddDraftTextPost
    {
        public Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = TumblrProvider.CreateTumblrApplicationCredentials();

            IAuthorizationInformation authorizationInformation = TumblrProvider.CreateTumblrAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory(applicationCredentials, authorizationInformation);

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.tumblr.com/v2/blog/photography.blacktau.com/post", HttpMethod.Post, OpenAuthVersion.OneA);

            openAuthClient.AddBodyParameter("type", "text");
            openAuthClient.AddBodyParameter("state", "draft");
            openAuthClient.AddBodyParameter("title", "A test from Blacktau.OpenAuth");
            openAuthClient.AddBodyParameter("body", "just a test post from Blacktau.OpenAuth to see if everything works. Ԑթל֍ﭠ");

            return openAuthClient.Execute();
        }
    }
}