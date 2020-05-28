using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GameSalesApi.Middleware.RedirectorMiddleware
{
    public class RedirectorMiddleware
    {
        private readonly Redirector redirector;
        private readonly RequestDelegate next;
        public RedirectorMiddleware(RequestDelegate next, Redirector redirector)
        {
            this.redirector = redirector;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (redirector.RouteExists(context.Request.Path))
            {
                var result = await redirector.RedirectRequest(context.Request);
                context.Response.StatusCode = (int)result.StatusCode;
                context.Response.ContentLength = result.Content.Headers.ContentLength;
                context.Response.Headers.Add("Content-Type",result.Content.Headers.ContentType.MediaType);
                await context.Response.WriteAsync(await result.Content.ReadAsStringAsync());
                return;
            }
            await next(context);
        }
    }
}
