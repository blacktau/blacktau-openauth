namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.Client.Interfaces;

    public interface IParameterSetFactory
    {
        IDictionary<string, string> GetRequestTokenParameters(IApplicationCredentials credentials, string callback);
    }
}