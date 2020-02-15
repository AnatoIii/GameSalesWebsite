using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GatewayAPI.RedirectorMiddleware
{
    public class Redirector
    {
        static HttpClient httpClient = new HttpClient();
        readonly RoutesConfig configuration;
        private readonly ILogger<Redirector> logger;
        public Redirector(IOptions<RoutesConfig> options, ILogger<Redirector> logger) {
            configuration = options.Value;
            this.logger = logger;
        }
        
        public bool RouteExists(PathString path)
        {
            var route = path.ToString().Split('/')[1];
            return configuration.Routes.Where(r => r.TriggerRoute == route).Any();
        }

        public async Task<HttpResponseMessage> RedirectRequest(HttpRequest httpRequest)
        {
            var newRequestMessage = HttpMessageCreator.PrepareRequestMessage(httpRequest, configuration);
            using (StreamReader reader = new StreamReader(httpRequest.Body, Encoding.UTF8))
            {
                newRequestMessage.Content = new StringContent(await reader.ReadToEndAsync());
                HttpResponseMessage response;

                try {
                    response = await httpClient.SendAsync(newRequestMessage);
                } catch (HttpRequestException e)
                {
                    logger.LogError("Unable to connect to service. URI:{0}, Method:{1}, ErrorMessage:{2}", newRequestMessage.RequestUri, newRequestMessage.Method,e.Message);
                    response = HttpMessageCreator.PrepareErrorResponseMessage();
                }
                return response;
            }
        }
    }
}
