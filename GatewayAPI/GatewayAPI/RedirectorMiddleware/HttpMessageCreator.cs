using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GatewayAPI.RedirectorMiddleware
{
    public class HttpMessageCreator
    {
        public static HttpRequestMessage PrepareRequestMessage(HttpRequest httpRequest, RoutesConfig routesConfig)
        {
            var route = httpRequest.Path.ToString().Split('/')[1];
            string newPath = routesConfig.Routes.Where(r => r.TriggerRoute == route).Select(r => r.URL).FirstOrDefault();
            newPath += httpRequest.Path.ToString().Substring(route.Length + 1);

            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(httpRequest.Method), newPath);
            requestMessage.Headers.Add("GatewayHostKey", routesConfig.GatewayHostKey);

            return requestMessage;
        }

        public static HttpResponseMessage PrepareErrorResponseMessage()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            responseMessage.Content = new StringContent("Service temporarily unavailable");
            return responseMessage;
        }
    }
}
