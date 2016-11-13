namespace Blacktau.OpenAuth.AspNet.Authorization.Twitter
{
    using Blacktau.OpenAuth.Client;

    public class TwitterAuthorizationInformation : AuthorizationInformation
    {
        public string ScreenName { get; set; }

        public string UserId { get; set; }
    }
}