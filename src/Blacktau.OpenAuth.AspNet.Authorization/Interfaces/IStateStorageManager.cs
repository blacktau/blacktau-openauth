namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IStateStorageManager
    {
        void Store<T>(HttpContext httpContext, string key, T value);

        T Retrieve<T>(HttpContext httpContext, string key);
    }
}