namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IVersionTwoCallbackHandler
    {
        Task<Task> HandleCallBack(HttpContext context, IVersionTwoOpenAuthorizationOptions options);
    }
}