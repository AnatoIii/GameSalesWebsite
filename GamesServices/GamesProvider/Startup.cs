using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBAccess;
using GamesProvider.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;

namespace GamesProvider
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
            services.AddMvc();
            services.AddDbContext<GameServiceDBContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("Games"));
            });
            services.AddTransient(typeof(IGamesPricesService), typeof(GamesPricesService));
            services.AddTransient(typeof(IGameService), typeof(GameService));
            services.AddTransient(typeof(IPlatformService), typeof(PlatfromService));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
