namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA
{
    using Microsoft.AspNetCore.Http;

    public interface IRequestTokenStorageManager
    {
        void StoreRequestTokenSecret(HttpContext context, string requestToken, string requestTokenSecret);

        string RetrieveRequestTokenSecret(HttpContext context, string requestToken);
    }
}