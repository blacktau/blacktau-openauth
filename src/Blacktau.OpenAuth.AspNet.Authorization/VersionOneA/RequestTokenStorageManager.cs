namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using System;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;

    using Microsoft.AspNetCore.Http;

    public class RequestTokenStorageManager : IRequestTokenStorageManager
    {
        private const string RequestTokenResponseKey = "RequestTokenResponse";

        private readonly IStateStorageManager storageManager;

        public RequestTokenStorageManager(IStateStorageManager storageManager)
        {
            this.storageManager = storageManager;
        }

        public string RetrieveRequestTokenSecret(HttpContext context, string requestToken)
        {
            var pair = this.storageManager.Retrieve<Tuple<string, string>>(context, RequestTokenResponseKey);
            return pair?.Item1 == requestToken ? pair?.Item2 : null;
        }

        public void StoreRequestTokenSecret(HttpContext context, string requestToken, string requestTokenSecret)
        {
            this.storageManager.Store(context, RequestTokenResponseKey, new Tuple<string, string>(requestToken, requestTokenSecret));
        }
    }
}