namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features.Authentication;
    using Microsoft.Extensions.Logging;

    public interface IOpenAuthorizationHandler
    {
        Task HandleRequest(HttpContext context);

        Task TeardownAsync();
    }
}