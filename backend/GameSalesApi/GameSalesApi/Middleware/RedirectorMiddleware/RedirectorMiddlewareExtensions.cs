using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSalesApi.Middleware.RedirectorMiddleware
{
    public static class RedirectorMiddlewareExtensions
    {
        public static IApplicationBuilder UseRedirects(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RedirectorMiddleware>();
        }
    }
}
