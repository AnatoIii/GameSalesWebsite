using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameSalesApi.Middleware.RedirectorMiddleware
{
    /// <summary>
    /// Helper class, responsible for the creation of redirected requests and creating stub responses in case of an error
    /// </summary>
    public class HttpMessageCreator
    {
        /// <summary>
        /// Creates a request message for one of the internal services
        /// </summary>
        /// <param name="httpRequest">Incoming request</param>
        /// <param name="routesConfig">Redirector configuration</param>
        /// <returns>New internal request</returns>
        public static HttpRequestMessage PrepareRequestMessage(HttpRequest httpRequest, RoutesConfig routesConfig)
        {
            var route = httpRequest.Path.ToString().Split('/')[1];
            string newPath = routesConfig.Routes.Where(r => r.TriggerRoute == route).Select(r => r.URL).FirstOrDefault();
            newPath += httpRequest.Path.ToString().Substring(route.Length + 1);
            newPath += httpRequest.QueryString;

            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(httpRequest.Method), newPath);
            foreach(var header in httpRequest.Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value.ToArray());
            }
            requestMessage.Headers.Add("GatewayHostKey", routesConfig.GatewayHostKey);

            return requestMessage;
        }

        /// <summary>
        /// Creates a default response in case of a connection error
        /// </summary>
        public static HttpResponseMessage PrepareErrorResponseMessage()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            responseMessage.Content = new StringContent("Service temporarily unavailable");
            return responseMessage;
        }
    }
}
