namespace Blacktau.OpenAuth.AspNet.Authorization.VersionTwo
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo;
    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionTwo;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class VersionTwoCallbackHandler : IVersionTwoCallbackHandler
    {
        private readonly IVersionTwoAuthorizationParametersGenerator authorizationParametersGenerator;

        private readonly IOpenAuthClientFactory openAuthClientFactory;

        private readonly IStateStorageManager storageManager;

        private ILogger<VersionTwoCallbackHandler> logger;

        public VersionTwoCallbackHandler(ILoggerFactory loggerFactory, IStateStorageManager storageManager, IVersionTwoAuthorizationParametersGenerator authorizationParametersGenerator, IOpenAuthClientFactory openAuthClientFactory)
        {
            this.storageManager = storageManager;
            this.authorizationParametersGenerator = authorizationParametersGenerator;
            this.openAuthClientFactory = openAuthClientFactory;
            this.logger = loggerFactory.CreateLogger<VersionTwoCallbackHandler>();
        }

        public async Task<Task> HandleCallBack(HttpContext context, IVersionTwoOpenAuthorizationOptions options)
        {
            this.ValidateRequestState(context, options);

            if (IsErrorResponse(context))
            {
                return HandleError(context, options);
            }

            var code = GetQueryParameterValue(context, AuthorizationFieldNames.Code);

            var applicationCredentials = options.GetApplicationCredentials();

            var uriBuilder = new UriBuilder(context.Request.Scheme, context.Request.Host.Host, context.Request.Host.Port.GetValueOrDefault(), context.Request.Path);

            var codeForAccessTokenParameters = this.authorizationParametersGenerator.GetExchangeCodeForAccessTokenParameters(applicationCredentials, uriBuilder.Uri.AbsoluteUri, code);

            var client = this.openAuthClientFactory.CreateOpenAuthClient(options.AccessTokenEndpointUri, HttpMethod.Post, options.OpenAuthVersion, applicationCredentials, null);

            foreach (var tokenRequestParameter in codeForAccessTokenParameters)
            {
                client.AddBodyParameter(tokenRequestParameter.Key, tokenRequestParameter.Value);
            }

            var result = await client.Execute();

            var authorizationInformation = ParseAccessTokenResult(result, options);

            return options.SuccessHandler.Invoke(authorizationInformation, context);
        }

        private static string GetQueryParameterValue(HttpContext context, string fieldName)
        {
            if (context.Request.Query.ContainsKey(fieldName))
            {
                return context.Request.Query[fieldName];
            }

            return null;
        }

        private static Task HandleError(HttpContext context, IVersionTwoOpenAuthorizationOptions options)
        {
            var error = GetQueryParameterValue(context, AuthorizationFieldNames.Error);
            var errorReason = GetQueryParameterValue(context, AuthorizationFieldNames.ErrorReason);
            var errorDescription = GetQueryParameterValue(context, AuthorizationFieldNames.ErrorDescription);

            var errorException = new OpenAuthorizationVersionTwoResponseErrorException(error, errorReason, errorDescription);

            return options.FailureHandler.Invoke(errorException, context);
        }

        private static bool IsErrorResponse(HttpContext context)
        {
            return context.Request.Query.ContainsKey(AuthorizationFieldNames.Error);
        }

        private static IAuthorizationInformation ParseAccessTokenResult(string result, IVersionTwoOpenAuthorizationOptions authorizationOptions)
        {
            var resultParameters = result.QueryParameterStringToDictionary();
            return authorizationOptions.ExtractAuthorizationInformation(resultParameters);
        }

        private static string GetRequestState(HttpContext context)
        {
            if (context.Request.Query.ContainsKey(AuthorizationFieldNames.State))
            {
                return context.Request.Query[AuthorizationFieldNames.State];
            }

            return string.Empty;
        }

        private void ValidateRequestState(HttpContext context, IVersionTwoOpenAuthorizationOptions options)
        {
            var state = GetRequestState(context);
            var storageState = this.storageManager.Retrieve<string>(context, options.RequestStateStorageKey);
            if (!storageState.Equals(state))
            {
                throw new Exception("Invalid Authorization State");
            }
        }
    }
}