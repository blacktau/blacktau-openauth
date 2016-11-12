namespace Blacktau.OpenAuth.AspNet.Authorization.VersionTwo
{
    using System;

    public class OpenAuthorizationVersionTwoResponseErrorException : Exception
    {
        private readonly string error;

        private readonly string errorReason;

        private readonly string errorDescription;

        public OpenAuthorizationVersionTwoResponseErrorException(string error, string errorReason, string errorDescription)
        {
            this.error = error;
            this.errorReason = errorReason;
            this.errorDescription = errorDescription;
        }

        public string Error => this.error;

        public string ErrorReason => this.errorReason;

        public string ErrorDescription => this.errorDescription;
    }
}