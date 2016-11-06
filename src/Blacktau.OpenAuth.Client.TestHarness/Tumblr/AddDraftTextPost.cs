namespace Blacktau.OpenAuth.Client.TestHarness.Tumblr
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Containers.Basic;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class AddDraftTextPost
    {
        private readonly TumblrProvider tumblrProvider;

        public AddDraftTextPost(TumblrProvider tumblrProvider)
        {
            this.tumblrProvider = tumblrProvider;
        }

        public Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = this.tumblrProvider.CreateTumblrApplicationCredentials();

            IAuthorizationInformation authorizationInformation = this.tumblrProvider.CreateTumblrAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory();

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.tumblr.com/v2/blog/photography.blacktau.com/post", HttpMethod.Post, OpenAuthVersion.OneA, applicationCredentials, authorizationInformation);

            openAuthClient.AddBodyParameter("type", "text");
            openAuthClient.AddBodyParameter("state", "draft");
            openAuthClient.AddBodyParameter("title", "A test from Blacktau.OpenAuth");
            openAuthClient.AddBodyParameter("body", "just a test post from Blacktau.OpenAuth to see if everything works. Ԑթל֍ﭠ");

            return openAuthClient.Execute();
        }
    }
}