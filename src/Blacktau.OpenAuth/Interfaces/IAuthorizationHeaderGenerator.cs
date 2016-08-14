namespace Blacktau.OpenAuth.Interfaces
{
    using System.Collections.Generic;

    public interface IAuthorizationHeaderGenerator
    {
        string GenerateHeaderValue(
            IApplicationCredentials applicationCredentials, 
            IDictionary<string, string> queryParameters, 
            IDictionary<string, string> bodyParameters, 
            IAuthorizationInformation authorizationInformation, 
            HttpMethod method,
            string url);
    }
}