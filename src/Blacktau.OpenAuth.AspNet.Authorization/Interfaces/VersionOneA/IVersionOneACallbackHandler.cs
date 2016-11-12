namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IVersionOneACallbackHandler
    {
        Task HandleCallBack(HttpContext context, IVersionOneAOpenAuthorizationOptions options);
    }
}