namespace Blacktau.OpenAuth.AspNet.Authorization.Twitter
{
    using Blacktau.OpenAuth.Client;
    public class TwitterAuthorizationInformation : AuthorizationInformation
    {
        public TwitterAuthorizationInformation(string accessToken)
            : base(accessToken)
        {
        }

        public string UserId { get; set; }

        public string ScreenName { get; set; }
    }
}