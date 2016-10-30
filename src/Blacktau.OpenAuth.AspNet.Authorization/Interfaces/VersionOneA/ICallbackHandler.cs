namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICallbackHandler
    {
        Task HandleCallBack(HttpContext context, OpenAuthorizationOptions options);
    }
}