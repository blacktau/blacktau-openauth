namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;

    public class CallbackHandler : ICallbackHandler
    {
        private readonly IRequestTokenStorageManager storageManager;

        private readonly IOpenAuthClientFactory openAuthClientFactory;

        private ILogger<CallbackHandler> logger;

        public CallbackHandler(ILoggerFactory loggerFactory, IRequestTokenStorageManager storageManager, IOpenAuthClientFactory openAuthClientFactory)
        {
            this.storageManager = storageManager;
            this.openAuthClientFactory = openAuthClientFactory;
            this.logger = loggerFactory.CreateLogger<CallbackHandler>();
        }

        public async Task HandleCallBack(HttpContext context, OpenAuthorizationOptions options)
        {
            if (this.IsDenied(context))
            {
                var exception = new Exception("Authorization Denied.");
                await options.FailureHandler.Invoke(exception, context);
                return;
            }

            await this.RequestAccessToken(context, options);
        }

        private static string GetQueryParameterValue(HttpContext context, string name)
        {
            StringValues values;
            if (context.Request.Query.TryGetValue(name, out values))
            {
                return values.FirstOrDefault();
            }

            return string.Empty;
        }
        
        private async Task RequestAccessToken(HttpContext context, OpenAuthorizationOptions options)
        {
            var requestToken = GetQueryParameterValue(context, AuthorizationFieldNames.Token);
           
            var requestTokenSecret = this.storageManager.RetrieveRequestTokenSecret(context, requestToken);
            
            var oauthVerifier = GetQueryParameterValue(context, AuthorizationFieldNames.Verifier);

            var authorizationInformation = new AuthorizationInformation(requestToken);

            var client = this.openAuthClientFactory.CreateOpenAuthClient(options.AccessTokenEndpointUri, HttpMethod.Post, OpenAuthVersion.OneA, options.ApplicationCredentials, authorizationInformation);
            client.AddAdditionalAuthorizationParameter(AuthorizationFieldNames.Verifier, oauthVerifier);

            var response = await client.Execute();
            
        }

        private bool IsDenied(HttpContext context)
        {
            var denighed = GetQueryParameterValue(context, AuthorizationFieldNames.Denied);
            return !string.IsNullOrWhiteSpace(denighed);
        }
    }
}