using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayAPI.RedirectorMiddleware
{
    public class RoutesConfig
    {
        public Route[] Routes { get; set; }
        public string GatewayHostKey { get; set; }
    }
}
