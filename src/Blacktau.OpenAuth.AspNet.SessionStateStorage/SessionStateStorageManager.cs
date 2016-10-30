namespace Blacktau.OpenAuth.AspNet.SessionStateStorage
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.AspNetCore.Http;

    using Newtonsoft.Json;

    public class SessionStateStorageManager : IStateStorageManager
    {
        public T Retrieve<T>(HttpContext httpContext, string key)
        {
            var valueString = httpContext.Session.GetString(key);
            return JsonConvert.DeserializeObject<T>(valueString);
        }

        public void Store<T>(HttpContext httpContext, string key, T value)
        {
            var valueString = JsonConvert.SerializeObject(value);
            httpContext.Session.SetString(key, valueString);
        }
    }
}