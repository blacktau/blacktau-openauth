namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IAuthorizationRequestor
    {
        Task RequestAuthorization(HttpContext context, OpenAuthorizationOptions options);
    }
}