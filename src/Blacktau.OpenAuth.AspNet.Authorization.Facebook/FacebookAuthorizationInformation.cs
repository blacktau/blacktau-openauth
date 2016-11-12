namespace Blacktau.OpenAuth.AspNet.Authorization.Facebook
{
    using Blacktau.OpenAuth.Client;

    public class FacebookAuthorizationInformation : AuthorizationInformation
    {
        public FacebookAuthorizationInformation(string accessToken)
            : base(accessToken)
        {
        }
    }
}