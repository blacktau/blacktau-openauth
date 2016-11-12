namespace Blacktau.OpenAuth.AspNet.Authorization.VersionTwo
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionTwo;

    public class VersionTwoAuthorizationParametersGenerator : IVersionTwoAuthorizationParametersGenerator
    {
        public IDictionary<string, string> GetAuthorizationRequestParameters(IApplicationCredentials appCredentials, string redirectUri, string scope, string stateValue)
        {
            return new Dictionary<string, string>
                        {
                            { AuthorizationFieldNames.ClientId, appCredentials.ApplicationKey },
                            { AuthorizationFieldNames.RedirectUri, redirectUri },
                            { AuthorizationFieldNames.Scope, scope },
                            { AuthorizationFieldNames.State, stateValue },
                            { AuthorizationFieldNames.ResponseType, "code" }
                        };
        }

        public IDictionary<string, string> GetExchangeCodeForAccessTokenParameters(IApplicationCredentials credentials, string redirectUri, string code, string grantType = "authorization_code")
        {
            return new Dictionary<string, string>
                        {
                            { AuthorizationFieldNames.Code, code },
                            { AuthorizationFieldNames.ClientId, credentials.ApplicationKey },
                            { AuthorizationFieldNames.RedirectUri, redirectUri },
                            { AuthorizationFieldNames.ClientSecret, credentials.ApplicationSecret },
                            { AuthorizationFieldNames.GrantType, grantType }
                        };
        }
    }
}