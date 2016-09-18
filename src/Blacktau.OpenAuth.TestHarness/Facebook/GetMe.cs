namespace Blacktau.OpenAuth.TestHarness.Facebook
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Interfaces;

    public class GetMe
    {
        public Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = FacebookProvider.CreateFacebookApplicationCredentials();

            IAuthorizationInformation authorizationInformation = FacebookProvider.CreateFacebookAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory();

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://graph.facebook.com/v2.7/me", HttpMethod.Get, OpenAuthVersion.Two, applicationCredentials, authorizationInformation);

            return openAuthClient.Execute();
        }
    }
}