﻿namespace Blacktau.OpenAuth.TestHarness.Twitter.Lists
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Basic;
    using Blacktau.OpenAuth.Interfaces;

    public class DestroyList
    {
        public async Task<string> Execute(string id)
        {
            IApplicationCredentials applicationCredentials = TwitterProvider.CreateTwitterApplicationCredentials();

            IAuthorizationInformation authorizationInformation = TwitterProvider.CreateTwitterAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory(applicationCredentials, authorizationInformation);

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.twitter.com/1.1/lists/destroy.json", HttpMethod.Post, OpenAuthVersion.OneA);

            openAuthClient.AddQueryParameter("list_id", id);

            var result = await openAuthClient.Execute();
            return result;
        }
    }
}