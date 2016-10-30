namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    public interface IOpenAuthorizationFeature
    {
        IOpenAuthorizationHandler Handler { get; set; }
    }
}