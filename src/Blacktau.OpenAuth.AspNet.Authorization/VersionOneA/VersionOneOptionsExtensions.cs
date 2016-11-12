namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;

    internal static class VersionOneOptionsExtensions
    {
        public static IApplicationCredentials GetApplicationCredentials(this IOpenAuthorizationOptions options)
        {
            return new ApplicationCredentials { ApplicationKey = options.ApplicationKey, ApplicationSecret = options.ApplicationSecret };
        }
    }
}