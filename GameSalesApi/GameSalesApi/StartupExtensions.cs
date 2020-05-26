using System.Text;
using DataAccess;
using GameSalesApi.Features.Authorization;
using GameSalesApi.Middleware.RedirectorMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace GameSalesApi
{
    public static class StartupExtensions
    {
        public static IServiceCollection ConfigureProjectOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<RoutesConfig>(configuration.GetSection("redirects"));
            services.Configure<TokenConfig>(configuration.GetSection("TokenConfig"));
            services.Configure<ImgurConfig>(configuration.GetSection("ImgurApi"));
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<Redirector>();
            services.AddSingleton<TokenCreator>();
            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });
            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration.GetSection("TokenConfig")["Secret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                //options.Events = new JwtBearerEvents
                //{
                //    OnAuthenticationFailed = (context) =>
                //    {
                //        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                //        {
                //            context.Response.Headers.Add("Expired", "true");
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
            });
            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<GameSalesContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionStrings:GameSalesContext"]);
            });
            return services;
        }
    }
}
