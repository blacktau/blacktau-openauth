namespace Blacktau.OpenAuth.AspNet.Authorization.Flickr
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.AspNet.Authorization.VersionOneA;
    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    public class FlickrAuthorizationOptions : VersionOneOpenAuthorizationOptions
    {
        public FlickrAuthorizationOptions()
        {
            this.Description.DisplayName = "Flickr";
        }

        public override string AccessTokenEndpointUri => "https://www.flickr.com/services/oauth/access_token";

        public override string AuthorizeEndpointUri => "https://www.flickr.com/services/oauth/authorize";

        public string Key
        {
            get
            {
                return this.ApplicationKey;
            }

            set
            {
                this.ApplicationKey = value;
            }
        }

        public override string RequestStateStorageKey => "FlickrStateKey";

        public override string RequestTokenEndpointUri => "https://www.flickr.com/services/oauth/request_token";

        public string Secret
        {
            get
            {
                return this.ApplicationSecret;
            }

            set
            {
                this.ApplicationSecret = value;
            }
        }

        public override string ServiceProviderName => "Flickr";

        public override IAuthorizationInformation ExtractAuthorizationInformation(IDictionary<string, string> parameters)
        {
            var authorizationInformation = new FlickrAuthorizationInformation(null);

            authorizationInformation.AccessToken = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.Token);
            authorizationInformation.AccessTokenSecret = this.GetAuthorizationFieldValue(parameters, AuthorizationFieldNames.TokenSecret);

            authorizationInformation.Fullname = this.GetAuthorizationFieldValue(parameters, "fullname");
            authorizationInformation.Username = this.GetAuthorizationFieldValue(parameters, "username");
            authorizationInformation.UserNsid = this.GetAuthorizationFieldValue(parameters, "user_nsid");

            return authorizationInformation;
        }
    }
}