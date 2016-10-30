namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class AuthorizationRequestor : IAuthorizationRequestor
    {
        private readonly IOpenAuthClientFactory openAuthClientFactory;

        private readonly IRequestTokenStorageManager storageManager;

        private ILogger<AuthorizationRequestor> logger;

        public AuthorizationRequestor(IOpenAuthClientFactory openAuthClientFactory, IRequestTokenStorageManager storageManager, ILoggerFactory loggerFactory)
        {
            this.openAuthClientFactory = openAuthClientFactory;
            this.storageManager = storageManager;
            this.logger = loggerFactory.CreateLogger<AuthorizationRequestor>();
        }

        public async Task RequestAuthorization(HttpContext context, OpenAuthorizationOptions options)
        {
            var requestToken = await this.CreateRequestToken(context, options);
            this.RedirectToAuthorizationEndpoint(context, requestToken, options);
        }

        private async Task<string> CreateRequestToken(HttpContext context, OpenAuthorizationOptions options)
        {
            var callBackUrl = options.GetCallBackUrl(context);
            var requestTokenResponse = await this.RequestTokenAsync(callBackUrl, options, context);
            return requestTokenResponse[AuthorizationFieldNames.Token];
        }

        private Uri GetAuthorizationUri(string requestToken, OpenAuthorizationOptions options)
        {
            var authorizeEndpointUri = options.AuthorizeEndpointUri;
            var uriBuilder = new UriBuilder(authorizeEndpointUri);
            uriBuilder.Query = (uriBuilder.Query.Length == 0 ? string.Empty : uriBuilder.Query + UriConstants.AmpersandDelimiter) + AuthorizationFieldNames.Token + UriConstants.EqualsDelimiter + requestToken;
            return uriBuilder.Uri;
        }

        private RequestTokenNotConfirmedException ProcessRequestTokenNotConfirmedResponse(IDictionary<string, string> responseParameters)
        {
            return new RequestTokenNotConfirmedException(Resources.Exception_MissingRequestToken, responseParameters);
        }

        private void RedirectToAuthorizationEndpoint(HttpContext context, string requestToken, OpenAuthorizationOptions options)
        {
            var authorizationUri = this.GetAuthorizationUri(requestToken, options);
            context.Response.Redirect(authorizationUri.AbsoluteUri);
        }

        private async Task<IDictionary<string, string>> RequestTokenAsync(string callbackUrl, OpenAuthorizationOptions options, HttpContext context)
        {
            var openAuthClient = this.openAuthClientFactory.CreateOpenAuthClient(options.RequestTokenEndpointUri, HttpMethod.Post, OpenAuthVersion.OneA, options.ApplicationCredentials, null);
            openAuthClient.AddAdditionalAuthorizationParameter(AuthorizationFieldNames.Callback, callbackUrl);

            var response = await openAuthClient.Execute();
            var result = response.QueryParameterStringToDictionary();

            var confirmed = string.Compare("true", result[AuthorizationFieldNames.CallbackConfirmed], StringComparison.OrdinalIgnoreCase) == 0;

            if (!confirmed)
            {
                throw this.ProcessRequestTokenNotConfirmedResponse(result);
            }

            this.storageManager.StoreRequestTokenSecret(context, result[AuthorizationFieldNames.Token], result[AuthorizationFieldNames.TokenSecret]);
            return result;
        }
    }
}