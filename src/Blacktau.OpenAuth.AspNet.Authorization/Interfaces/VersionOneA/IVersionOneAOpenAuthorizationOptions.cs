namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA
{
    public interface IVersionOneAOpenAuthorizationOptions : IOpenAuthorizationOptions
    {
        string RequestTokenEndpointUri { get; }
    }
}