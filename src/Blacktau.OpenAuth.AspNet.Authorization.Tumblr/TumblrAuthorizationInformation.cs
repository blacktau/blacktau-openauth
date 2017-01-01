namespace Blacktau.OpenAuth.AspNet.Authorization.Tumblr
{
    using Blacktau.OpenAuth.Client;

    public class TumblrAuthorizationInformation : AuthorizationInformation
    {
        public TumblrAuthorizationInformation(string accessToken)
            : base(accessToken)
        {
        }

        public string UserId { get; set; }

        public string ScreenName { get; set; }
    }
}