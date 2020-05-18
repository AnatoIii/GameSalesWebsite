using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBAccess;
using GamesSaver.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerBackgroundServices;

namespace GamesSaver
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
            services.AddOptions();
            services.Configure<QueueSettings>(Configuration.GetSection("queueSettings"));
            services.AddDbContext<GameServiceDBContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("Games"));
            });
            services.AddTransient(typeof(IGameService),typeof(GameService));
            services.AddTransient(typeof(IGamesPricesService),typeof(GamesPricesService));
            services.AddHostedService<QueueReaderBackgroundService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
