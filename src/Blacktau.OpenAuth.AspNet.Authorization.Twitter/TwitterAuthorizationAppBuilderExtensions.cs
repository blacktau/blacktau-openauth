namespace Blacktau.OpenAuth.AspNet.Authorization.Twitter
{
    using System;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Options;

    public static class TwitterAuthorizationAppBuilderExtensions
    {
        public static IApplicationBuilder UseTwitterAuthorization(this IApplicationBuilder app, TwitterAuthorizationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.UseOAuthAuthorization(options);

            IOpenAuthorizationResourceProviderRegistery registery = app.ApplicationServices.GetService(typeof(IOpenAuthorizationResourceProviderRegistery)) as IOpenAuthorizationResourceProviderRegistery;
            
            registery.Register(options);

            return app.UseMiddleware<OpenAuthorizationMiddleware<OpenAuthorizationOptions>>(Options.Create(options));
        }
    }
}