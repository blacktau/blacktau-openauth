namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IUrlValidator
    {
        bool IsRelevantRequest(HttpContext context, IOpenAuthorizationOptions options);

        bool IsAuthorizationRequest(HttpContext context, IOpenAuthorizationOptions options);

        bool IsAuthorizationCallbackRequest(HttpContext context, IOpenAuthorizationOptions options);

        string GetCallbackUrl(HttpContext context, IOpenAuthorizationOptions options);

        string GetActivationPath(IOpenAuthorizationOptions options);
    }
}