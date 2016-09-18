namespace Blacktau.OpenAuth.Interfaces
{
    public interface IAuthorizationInformation
    {
        string AccessTokenSecret { get; set; }

        string AccessToken { get; set; }
    }
}