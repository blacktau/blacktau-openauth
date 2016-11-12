namespace Blacktau.OpenAuth.Client.VersionTwo
{
    using System.Net.Http.Headers;

    using Blacktau.OpenAuth.Client.Interfaces;

    public class AuthorizationHeaderGenerator : IOpenAuthVersionTwoAuthorizationHeaderGenerator
    {
        public AuthenticationHeaderValue GenerateHeaderValue(IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation, IOpenAuthClient openAuthClient)
        {
            if (authorizationInformation == null)
            {
                return null;
            }

            return new AuthenticationHeaderValue(AuthorizationFieldNames.AuthorizationHeaderStart, authorizationInformation.AccessToken);
        }
    }
}