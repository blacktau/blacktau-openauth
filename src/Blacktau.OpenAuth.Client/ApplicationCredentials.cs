namespace Blacktau.OpenAuth.Client
{
    using Blacktau.OpenAuth.Client.Interfaces;

    public class ApplicationCredentials : IApplicationCredentials
    {
        public string ApplicationKey { get; set; }

        public string ApplicationSecret { get; set; }
    }
}