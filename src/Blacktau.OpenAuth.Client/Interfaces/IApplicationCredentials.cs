namespace Blacktau.OpenAuth.Client.Interfaces
{
    public interface IApplicationCredentials
    {
        string ApplicationKey { get; set; }

        string ApplicationSecret { get; set; }
    }
}