namespace Blacktau.OpenAuth.VersionTwo
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.Interfaces;

    public class AuthorizationHeaderGenerator : IAuthorizationHeaderGenerator
    {
        public string GenerateHeaderValue(IApplicationCredentials applicationCredentials,
                                          IDictionary<string, string> queryParameters,
                                          IDictionary<string, string> bodyParameters,
                                          IAuthorizationInformation authorizationInformation,
                                          HttpMethod method,
                                          string url)
        {
            return null;
        }
    }
}