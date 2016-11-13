namespace Blacktau.OpenAuth.Client
{
    using System;

    using Blacktau.OpenAuth.Client.Interfaces;

    public class AuthorizationInformation : IAuthorizationInformation
    {
        public string AccessTokenSecret { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? Expires { get; set; }
    }
}