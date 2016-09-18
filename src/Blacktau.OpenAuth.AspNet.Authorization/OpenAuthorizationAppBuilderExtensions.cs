namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Options;

    public static class OpenAuthorizationAppBuilderExtensions
    {
        public static IApplicationBuilder UseOAuthAuthorization(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<OpenAuthorizationMiddleware<OpenAuthorizationOptions>>();
        }

        public static IApplicationBuilder UseOAuthAuthorization(this IApplicationBuilder app, OpenAuthorizationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            return app.UseMiddleware<OpenAuthorizationMiddleware<OpenAuthorizationOptions>>(Options.Create(options));
        }

    }
}