namespace Blacktau.OpenAuth
{
    using Blacktau.OpenAuth.Interfaces;

    public class ApplicationCredentials : IApplicationCredentials
    {
        public string ApplicationKey { get; set; }

        public string ApplicationSecret { get; set; }
    }
}