namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    public interface IOpenAuthorizationHandlerFactory
    {
        IOpenAuthorizationHandler CreateHandler(IOpenAuthorizationOptions options);
    }
}