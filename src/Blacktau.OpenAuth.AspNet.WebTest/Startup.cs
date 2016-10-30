namespace Blacktau.OpenAuth.WebTest
{
    using Blacktau.OpenAuth.AspNet.Authorization;
    using Blacktau.OpenAuth.AspNet.Authorization.Twitter;
    using Blacktau.OpenAuth.AspNet.SessionStateStorage;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseTwitterAuthorization(new TwitterAuthorizationOptions() { ConsumerKey = "lBdRWTOhTX12lH92QrNvPqLpE", ConsumerSecret = "7OOsw0qgmGjadqOmeF52pmEGYrtJrv9rpel2PCvaoCFuh8U9nW" });

            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting();
            services.AddOpenAuthorization();
            services.AddSession();
            services.AddOpenAuthorizationSessionStorage();
        }
    }
}