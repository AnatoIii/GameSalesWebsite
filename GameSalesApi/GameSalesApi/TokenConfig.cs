using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSalesApi
{
    public class TokenConfig
    {
        public int JWTLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
        public string Secret { get; set; }
    }
}
