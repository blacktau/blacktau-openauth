namespace Blacktau.OpenAuth.AspNet.SessionStateStorage
{
    using System.Text;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.AspNetCore.Http;

    using Newtonsoft.Json;

    public class SessionStateStorageManager : IStateStorageManager
    {
        public T Retrieve<T>(HttpContext httpContext, string key)
        {
            var valueString = GetString(httpContext.Session, key);
            return JsonConvert.DeserializeObject<T>(valueString);
        }

        public void Store<T>(HttpContext httpContext, string key, T value)
        {
            var valueString = JsonConvert.SerializeObject(value);
            SetString(httpContext.Session, key, valueString);
        }

        private static byte[] GetValue(ISession session, string key)
        {
            byte[] numArray;
            session.TryGetValue(key, out numArray);
            return numArray;
        }

        private static string GetString(ISession session, string key)
        {
            var bytes = GetValue(session, key);
            if (bytes == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(bytes);
        }

        private static void SetString(ISession session, string key, string value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value));
        }
    }
}