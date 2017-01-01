namespace Blacktau.OpenAuth.AspNet.Authorization.Tumblr
{
    using System;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Options;

    public static class TumblrAuthorizationAppBuilderExtensions
    {
        public static IApplicationBuilder UseTumblrAuthorization(this IApplicationBuilder app, TumblrAuthorizationOptions options)
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

            return app.UseMiddleware<OpenAuthorizationMiddleware<TumblrAuthorizationOptions>>(Options.Create(options));
        }
    }
}