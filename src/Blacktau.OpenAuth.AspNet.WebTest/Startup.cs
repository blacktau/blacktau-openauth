namespace Blacktau.OpenAuth.WebTest
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization;
    using Blacktau.OpenAuth.AspNet.Authorization.Facebook;
    using Blacktau.OpenAuth.AspNet.Authorization.Tumblr;
    using Blacktau.OpenAuth.AspNet.Authorization.Twitter;
    using Blacktau.OpenAuth.AspNet.SessionStateStorage;
    using Blacktau.OpenAuth.Client.Interfaces;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                configurationBuilder.AddUserSecrets();
            }

            this.Configuration = configurationBuilder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseTwitterAuthorization(
                new TwitterAuthorizationOptions
                    {
                        ConsumerKey = this.GetRequiredConfigurationValue("Authorization:Twitter:ConsumerKey"),
                        ConsumerSecret = this.GetRequiredConfigurationValue("Authorization:Twitter:ConsumerSecret"),
                        SuccessHandler = this.TwitterSuccessHandler
                    });

            app.UseFacebookAuthorization(
                new FacebookAuthorizationOptions
                    {
                        ApplicationId = this.GetRequiredConfigurationValue("Authorization:Facebook:ApplicationId"),
                        ApplicationSecret = this.GetRequiredConfigurationValue("Authorization:Facebook:ApplicationSecret"),
                        Scope = {
                                   "publish_actions", "publish_pages" 
                                },
                        SuccessHandler = this.FacebookSuccessHandler
                    });

            app.UseTumblrAuthorization(
                new TumblrAuthorizationOptions
                    {
                        ConsumerKey = this.GetRequiredConfigurationValue("Authorization:Tumblr:ConsumerKey"),
                        ConsumerSecret = this.GetRequiredConfigurationValue("Authorization:Tumblr:ConsumerSecret"),
                        SuccessHandler = this.TumblrSuccessHandler
                    });

            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting();
            services.AddOpenAuthorization();
            services.AddSession();
            services.AddOpenAuthorizationSessionStorage();
        }

        private string GetRequiredConfigurationValue(string configurationKey)
        {
            var value = this.Configuration[configurationKey];
            if (string.IsNullOrWhiteSpace(configurationKey))
            {
                throw new Exception($"Required Configuration '{configurationKey}' missing.");
            }

            return value;
        }

        private Task TumblrSuccessHandler(IAuthorizationInformation authorizationInformation, HttpContext httpContext)
        {
            // don't do this. put it in a database somewhere. 
            httpContext.Response.Cookies.Append("TumblrAuthorizationInformation", JsonConvert.SerializeObject(authorizationInformation));
            return Task.CompletedTask;
        }

        private Task TwitterSuccessHandler(IAuthorizationInformation authorizationInformation, HttpContext httpContext)
        {
            // don't do this. put it in a database somewhere. 
            httpContext.Response.Cookies.Append("TwitterAuthorizationInformation", JsonConvert.SerializeObject(authorizationInformation));
            return Task.CompletedTask;
        }

        private Task FacebookSuccessHandler(IAuthorizationInformation authorizationInformation, HttpContext httpContext)
        {
            // don't do this. put it in a database somewhere. 
            httpContext.Response.Cookies.Append("FacebookAuthorizationInformation", JsonConvert.SerializeObject(authorizationInformation));
            return Task.CompletedTask;
        }
    }
}