namespace Blacktau.OpenAuth.Interfaces
{
    using System.Collections.Generic;

    public interface IAuthorizationParametersGenerator
    {
        IDictionary<string, string> CreateStandardParameterSet(IApplicationCredentials credentials);

        IDictionary<string, string> GetAuthorizationParameters(IApplicationCredentials credentials, string accessToken);
    }
}