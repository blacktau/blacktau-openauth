namespace Blacktau.OpenAuth.Client.Interfaces
{
    public interface IAuthorizationInformation
    {
        string AccessTokenSecret { get; set; }

        string AccessToken { get; set; }
    }
}