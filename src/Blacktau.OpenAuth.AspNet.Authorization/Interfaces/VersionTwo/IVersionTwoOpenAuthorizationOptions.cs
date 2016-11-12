namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo
{
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    public interface IVersionTwoOpenAuthorizationOptions : IOpenAuthorizationOptions
    {
        List<string> Scope { get; }
    }
}