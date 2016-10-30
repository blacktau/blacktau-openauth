namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class OAuthResourceProviderDescription
    {
        private const string DisplayNamePropertyKey = "DisplayName";

        private const string OpenAuthProtocolVersionPropertyKey = "OpenAuthProtocolVersion";

        private const string ServiceProviderNamePropertyKey = "ServiceProviderName";

        public OAuthResourceProviderDescription()
        {
            this.Items = new Dictionary<string, object>(StringComparer.Ordinal);
        }

        public string ActivationPath { get; set; }

        public string DisplayName
        {
            get
            {
                return this.GetString(DisplayNamePropertyKey);
            }

            set
            {
                this.Items[DisplayNamePropertyKey] = value;
            }
        }

        public IDictionary<string, object> Items { get; }

        public string OpenAuthProtocolVersion
        {
            get
            {
                return this.GetString(OpenAuthProtocolVersionPropertyKey);
            }

            set
            {
                this.Items[OpenAuthProtocolVersionPropertyKey] = value;
            }
        }

        public string ServiceProviderName
        {
            get
            {
                return this.GetString(ServiceProviderNamePropertyKey);
            }

            set
            {
                this.Items[ServiceProviderNamePropertyKey] = value;
            }
        }

        private string GetString(string name)
        {
            object value;

            if (this.Items.TryGetValue(name, out value))
            {
                return Convert.ToString(value, CultureInfo.InvariantCulture);
            }

            return null;
        }
    }
}