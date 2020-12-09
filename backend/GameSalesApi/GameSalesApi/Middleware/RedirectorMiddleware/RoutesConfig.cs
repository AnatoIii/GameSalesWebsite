using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSalesApi.Middleware.RedirectorMiddleware
{
    /// <summary>
    /// Contains the necessary configuration data and the Gateway Identification key
    /// </summary>
    public class RoutesConfig
    {
        public Route[] Routes { get; set; }
        public string GatewayHostKey { get; set; }
    }
}
