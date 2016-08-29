namespace Blacktau.OpenAuth.VersionTwo
{
    using System.Net.Http.Headers;

    using Blacktau.OpenAuth.Interfaces;

    public class AuthorizationHeaderGenerator : IAuthorizationHeaderGenerator
    {
        public AuthenticationHeaderValue GenerateHeaderValue(IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation, IOpenAuthClient openAuthClient)
        {
            return new AuthenticationHeaderValue(AuthorizationFieldNames.AuthorizationHeaderStart, authorizationInformation.AccessToken);
        }
    }
}