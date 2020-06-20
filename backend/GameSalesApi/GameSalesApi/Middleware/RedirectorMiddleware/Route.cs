using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSalesApi.Middleware.RedirectorMiddleware
{
    /// <summary>
    /// Represents the trigger route to external URL binding
    /// </summary>
    public class Route
    {
        public string TriggerRoute { get; set; }
        public string URL {get; set;}
    }
}
