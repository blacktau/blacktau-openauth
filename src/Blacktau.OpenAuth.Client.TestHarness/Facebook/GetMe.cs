namespace Blacktau.OpenAuth.Client.TestHarness.Facebook
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Containers.Basic;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class GetMe
    {
        private readonly FacebookProvider provider;

        public GetMe(FacebookProvider provider)
        {
            this.provider = provider;
        }

        public Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = this.provider.CreateFacebookApplicationCredentials();

            IAuthorizationInformation authorizationInformation = this.provider.CreateFacebookAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory();

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://graph.facebook.com/v2.7/me", HttpMethod.Get, OpenAuthVersion.Two, applicationCredentials, authorizationInformation);

            return openAuthClient.Execute();
        }
    }
}