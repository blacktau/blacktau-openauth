namespace Blacktau.OpenAuth.Client.VersionOneA
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.Interfaces.VersionOneA;

    public class AuthorizationParametersGenerator : IAuthorizationParametersGenerator
    {
        private readonly INonceFactory nonceFactory;

        private readonly ITimeStampFactory timeStampFactory;

        public AuthorizationParametersGenerator(INonceFactory nonceFactory, ITimeStampFactory timeStampFactory)
        {
            if (nonceFactory == null)
            {
                throw new ArgumentNullException(nameof(nonceFactory));
            }

            if (timeStampFactory == null)
            {
                throw new ArgumentNullException(nameof(timeStampFactory));
            }

            this.nonceFactory = nonceFactory;
            this.timeStampFactory = timeStampFactory;
        }

        public IDictionary<string, string> CreateStandardParameterSet(IApplicationCredentials credentials)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }

            var result = new Dictionary<string, string>
                             {
                                 { AuthorizationFieldNames.ConsumerKey, credentials.ApplicationKey }, 
                                 { AuthorizationFieldNames.Nonce, this.GetNonce() }, 
                                 { AuthorizationFieldNames.SignatureMethod, this.GetSignatureMethod() }, 
                                 { AuthorizationFieldNames.TimeStamp, this.GetTimestampValue() }, 
                                 { AuthorizationFieldNames.Version, "1.0" }
                             };
            return result;
        }

        public IDictionary<string, string> GetAuthorizationParameters(IApplicationCredentials credentials, string accessToken)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }

/*
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_Invalid, nameof(accessToken)), nameof(accessToken));
            }
*/

            var result = this.CreateStandardParameterSet(credentials);

            if (accessToken != null)
            {
                result.Add(AuthorizationFieldNames.Token, accessToken);
            }

            return result;
        }

        private string GetNonce()
        {
            return this.nonceFactory.GenerateNonce();
        }

        private string GetSignatureMethod()
        {
            return "HMAC-SHA1";
        }

        private string GetTimestampValue()
        {
            return ((long)this.timeStampFactory.GetTimeSpanFromEpoch().TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }
    }
}