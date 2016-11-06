namespace Blacktau.OpenAuth.WebTest
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.AspNet.Authorization;
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
                        ConsumerKey = this.Configuration["Authorization:Twitter:ConsumerKey"],
                        ConsumerSecret = this.Configuration["Authorization:Twitter:ConsumerSecret"],
                        SuccessHandler = this.TwitterSuccessHandler
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

        private Task TwitterSuccessHandler(IAuthorizationInformation authorizationInformation, HttpContext httpContext)
        {
            // don't do this. put it in a database somewhere. 
            httpContext.Response.Cookies.Append("TwitterAuthorizationInformation", JsonConvert.SerializeObject(authorizationInformation));
            return Task.CompletedTask;
        }
    }
}