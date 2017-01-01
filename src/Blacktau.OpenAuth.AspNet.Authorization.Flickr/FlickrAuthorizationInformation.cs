namespace Blacktau.OpenAuth.AspNet.Authorization.Flickr
{
    using Blacktau.OpenAuth.Client;

    public class FlickrAuthorizationInformation : AuthorizationInformation
    {
        public FlickrAuthorizationInformation(string accessToken)
            : base(accessToken)
        {
        }

        public string Fullname { get; set; }

        public string Username { get; set; }

        public string UserNsid { get; set; }
    }
}