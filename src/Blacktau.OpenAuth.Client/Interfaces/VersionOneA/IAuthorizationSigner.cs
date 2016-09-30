namespace Blacktau.OpenAuth.Client.Interfaces.VersionOneA
{
    using System.Collections.Generic;

    public interface IAuthorizationSigner
    {
        string GetSignature(string applicationSecret, string accessTokenSecret, string url, string method, params IEnumerable<KeyValuePair<string, string>>[] parameters);
    }
}