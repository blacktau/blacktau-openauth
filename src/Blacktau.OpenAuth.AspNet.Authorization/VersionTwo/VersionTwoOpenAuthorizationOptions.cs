namespace Blacktau.OpenAuth.AspNet.Authorization.VersionTwo
{
    using System;
    using System.Collections.Generic;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionTwo;

    public abstract class VersionTwoOpenAuthorizationOptions : OpenAuthorizationOptions, IVersionTwoOpenAuthorizationOptions
    {
        public override sealed OpenAuthVersion OpenAuthVersion => OpenAuthVersion.Two;

        public abstract List<string> Scope { get; }

        public override IAuthorizationInformation ExtractAuthorizationInformation(IDictionary<string, string> parameters)
        {
            var authorizationInformation = new AuthorizationInformation
                                               {
                                                   RefreshToken = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.RefreshToken),
                                                   AccessToken = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.AccessToken),
                                                   Expires = this.GetExpiry(parameters, AuthorizationFieldNames.ExpiresIn)
                                               };

            return authorizationInformation;
        }

        protected DateTime? GetExpiry(IDictionary<string, string> parameters, string fieldName)
        {
            var fieldValue = this.GetAuthorizationFieldValue(parameters, fieldName);

            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                return null;
            }

            int seconds;
            if (int.TryParse(fieldValue, out seconds))
            {
                return DateTime.Now.AddSeconds(seconds);
            }

            return null;
        }
    }
}