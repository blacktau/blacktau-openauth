namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IVersionTwoAuthorizationRequestor
    {
        void RequestAuthorization(HttpContext context, IVersionTwoOpenAuthorizationOptions options);
    }
}