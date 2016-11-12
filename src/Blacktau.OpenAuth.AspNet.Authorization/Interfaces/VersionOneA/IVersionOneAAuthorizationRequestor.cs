namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IVersionOneAAuthorizationRequestor
    {
        Task RequestAuthorization(HttpContext context, IVersionOneAOpenAuthorizationOptions options);
    }
}