namespace Blacktau.OpenAuth.AspNet.Authorization.Flickr
{
    using System;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Options;

    public static class FlickrAuthorizationAppBuilderExtensions
    {
        public static IApplicationBuilder UseFlickrAuthorization(this IApplicationBuilder app, FlickrAuthorizationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            IOpenAuthorizationResourceProviderRegistery registery = app.ApplicationServices.GetService(typeof(IOpenAuthorizationResourceProviderRegistery)) as IOpenAuthorizationResourceProviderRegistery;

            registery?.Register(options);

            return app.UseMiddleware<OpenAuthorizationMiddleware<FlickrAuthorizationOptions>>(Options.Create(options));
        }
    }
}