namespace Blacktau.OpenAuth.Interfaces
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    public interface IAuthorizationHeaderGenerator
    {
        AuthenticationHeaderValue GenerateHeaderValue(IApplicationCredentials applicationCredentials, IAuthorizationInformation authorizationInformation, IOpenAuthClient openAuthClient);
    }

    public interface IOpenAuthVersionOneAAuthorizationHeaderGenerator : IAuthorizationHeaderGenerator
    {
    }

    public interface IOpenAuthVersionTwoAuthorizationHeaderGenerator : IAuthorizationHeaderGenerator
    {
    }
}