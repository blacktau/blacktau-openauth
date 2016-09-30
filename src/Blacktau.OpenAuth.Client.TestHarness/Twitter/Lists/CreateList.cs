namespace Blacktau.OpenAuth.Client.TestHarness.Twitter.Lists
{
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Containers.Basic;
    using Blacktau.OpenAuth.Client.Interfaces;

    public class CreateList
    {
        private readonly Regex idRegex = new Regex("\"id_str\":\"(.+?)\"");

        public async Task<string> Execute()
        {
            IApplicationCredentials applicationCredentials = TwitterProvider.CreateTwitterApplicationCredentials();

            IAuthorizationInformation authorizationInformation = TwitterProvider.CreateTwitterAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory();

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.twitter.com/1.1/lists/create.json", HttpMethod.Post, OpenAuthVersion.OneA, applicationCredentials, authorizationInformation);

            openAuthClient.AddQueryParameter("name", "test_list_magec");
            openAuthClient.AddQueryParameter("mode", "private");

            var result = await openAuthClient.Execute();

            if (this.idRegex.IsMatch(result))
            {
                var matches = this.idRegex.Matches(result);
                return matches[0].Groups[1].Value;
            }

            return string.Empty;
        }

    }
}