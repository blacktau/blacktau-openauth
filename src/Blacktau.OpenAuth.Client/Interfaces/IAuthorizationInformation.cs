namespace Blacktau.OpenAuth.Client.Interfaces
{
    using System;

    public interface IAuthorizationInformation
    {
        string AccessTokenSecret { get; set; }

        string AccessToken { get; set; }

        string RefreshToken { get; set; }

        DateTime? Expires { get; set; }
    }
}