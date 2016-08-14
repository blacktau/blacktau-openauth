namespace Blacktau.OpenAuth
{
    using Blacktau.OpenAuth.Interfaces;

    public class AuthorizationInformation : IAuthorizationInformation
    {
        public AuthorizationInformation(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        public string AccessTokenSecret { get; set; }

        public string AccessToken { get; set; }
    }
}