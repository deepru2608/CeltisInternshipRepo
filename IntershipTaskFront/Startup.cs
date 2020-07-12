using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntershipTaskFront.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntershipTaskFront
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServerSideBlazor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddHttpClient<ICenterService, CenterService>(options =>
            {
                options.BaseAddress = new Uri("https://localhost:5001/api/Items/");
                options.DefaultRequestHeaders.Add("User-Client", "Item Service");
            });
            services.AddHttpClient<ITokenService, TokenService>(options =>
            {
                options.BaseAddress = new Uri("https://localhost:5001/api/Token/");
                options.DefaultRequestHeaders.Add("User-Client", "Token Service");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{Id?}");
                endpoints.MapBlazorHub();
            });
        }
    }
}