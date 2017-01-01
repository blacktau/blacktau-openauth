namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;

    public class VersionOneACallbackHandler : IVersionOneACallbackHandler
    {
        private readonly IOpenAuthClientFactory openAuthClientFactory;

        private readonly IRequestTokenStorageManager storageManager;

        private ILogger<VersionOneACallbackHandler> logger;

        public VersionOneACallbackHandler(ILoggerFactory loggerFactory, IRequestTokenStorageManager storageManager, IOpenAuthClientFactory openAuthClientFactory)
        {
            this.storageManager = storageManager;
            this.openAuthClientFactory = openAuthClientFactory;
            this.logger = loggerFactory.CreateLogger<VersionOneACallbackHandler>();
        }

        public async Task HandleCallBack(HttpContext context, IVersionOneAOpenAuthorizationOptions options)
        {
            if (this.IsDenied(context))
            {
                var exception = new Exception("Authorization Denied.");
                await options.FailureHandler.Invoke(exception, context);
                return;
            }
            
            await this.RequestAccessToken(context, options);
        }

        private static IAuthorizationInformation ExtractAuthorizationInformation(IOpenAuthorizationOptions options, string response)
        {
            var responseParameters = response.QueryParameterStringToDictionary();

            var extractedAuthorizationInformation = options.ExtractAuthorizationInformation(responseParameters);

            return extractedAuthorizationInformation;
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

        private bool IsDenied(HttpContext context)
        {
            var denighed = GetQueryParameterValue(context, AuthorizationFieldNames.Denied);
            return !string.IsNullOrWhiteSpace(denighed);
        }

        private IOpenAuthClient PrepareRequestClient(HttpContext context, IVersionOneAOpenAuthorizationOptions options)
        {
            var requestToken = GetQueryParameterValue(context, AuthorizationFieldNames.Token);

            var requestTokenSecret = this.storageManager.RetrieveRequestTokenSecret(context, options, requestToken);

            if (requestTokenSecret == null)
            {
                throw new Exception("invalid request?");
            }

            var oauthVerifier = GetQueryParameterValue(context, AuthorizationFieldNames.Verifier);

            var authorizationInformation = new AuthorizationInformation(requestToken);
            authorizationInformation.AccessTokenSecret = requestTokenSecret;

            var applicationCredentials = options.GetApplicationCredentials();

            var client = this.openAuthClientFactory.CreateOpenAuthClient(options.AccessTokenEndpointUri, HttpMethod.Post, OpenAuthVersion.OneA, applicationCredentials, authorizationInformation);
            client.AddAdditionalAuthorizationParameter(AuthorizationFieldNames.Verifier, oauthVerifier);
            return client;
        }

        private async Task RequestAccessToken(HttpContext context, IVersionOneAOpenAuthorizationOptions options)
        {
            var client = this.PrepareRequestClient(context, options);

            var response = await client.Execute();

            var extractedAuthorizationInformation = ExtractAuthorizationInformation(options, response);

            await options.SuccessHandler.Invoke(extractedAuthorizationInformation, context);
        }
    }
}