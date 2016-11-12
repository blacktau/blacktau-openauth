namespace Blacktau.OpenAuth.AspNet.Authorization.VersionOneA
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    public abstract class VersionOneOpenAuthorizationOptions : OpenAuthorizationOptions, IVersionOneAOpenAuthorizationOptions
    {
        protected VersionOneOpenAuthorizationOptions()
        {
        }

        public override sealed OpenAuthVersion OpenAuthVersion => OpenAuthVersion.OneA;

        public abstract string RequestTokenEndpointUri { get; }

        public override IAuthorizationInformation ExtractAuthorizationInformation(IDictionary<string, string> parameters)
        {
            var authorizationInformation = new AuthorizationInformation(string.Empty);

            if (this.OpenAuthVersion == OpenAuthVersion.OneA)
            {
                authorizationInformation.AccessToken = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.Token);
                authorizationInformation.AccessTokenSecret = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.TokenSecret);
            }

            return authorizationInformation;
        }
    }
}