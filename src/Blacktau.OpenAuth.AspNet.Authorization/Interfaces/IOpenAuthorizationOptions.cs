namespace Blacktau.OpenAuth.AspNet.Authorization.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;

    using Microsoft.AspNetCore.Http;

    public interface IOpenAuthorizationOptions
    {
        string RequestStateStorageKey { get; }

        string AccessTokenEndpointUri { get;  }

        string ApplicationKey { get;  }

        string ApplicationSecret { get;  }

        string AuthorizeEndpointUri { get;  }

        OAuthResourceProviderDescription Description { get; }

        Func<Exception, HttpContext, Task> FailureHandler { get; set; }

        OpenAuthVersion OpenAuthVersion { get;  }

        string ServiceProviderName { get; }

        Func<IAuthorizationInformation, HttpContext, Task> SuccessHandler { get; set; }

        IAuthorizationInformation ExtractAuthorizationInformation(IDictionary<string, string> parameters);
    }
}