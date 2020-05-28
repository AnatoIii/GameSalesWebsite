using DBAccess;
using GamesSaver.Services;
using GamesSaver.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

            services.AddTransient(typeof(IGameService),typeof(GameService));
            services.AddTransient(typeof(IGamesPricesService),typeof(GamesPricesService));

            services.AddDbContext<GameServiceDBContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("Games"));
            });

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
