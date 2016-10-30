namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RequestTokenNotConfirmedException : Exception
    {
        public RequestTokenNotConfirmedException(string message, IDictionary<string, string> parameters)
            : base(message)
        {
            this.ResponseParameters = parameters;
        }

        public IDictionary<string, string> ResponseParameters { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (this.ResponseParameters != null)
            {
                foreach (var responseParameter in this.ResponseParameters)
                {
                    builder.AppendFormat("{0}={1}", responseParameter.Key, responseParameter.Value).AppendLine();
                }
            }

            return $"{base.ToString()}\n{builder}";
        }
    }
}