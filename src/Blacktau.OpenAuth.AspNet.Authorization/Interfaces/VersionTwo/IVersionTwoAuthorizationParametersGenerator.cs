namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.Client.Interfaces;

    public interface IVersionTwoAuthorizationParametersGenerator
    {
        IDictionary<string, string> GetAuthorizationRequestParameters(IApplicationCredentials appCredentials, string redirectUri, string scope, string stateValue);

        IDictionary<string, string> GetExchangeCodeForAccessTokenParameters(IApplicationCredentials credentials, string redirectUri, string code, string grantType = "authorization_code");
    }
}