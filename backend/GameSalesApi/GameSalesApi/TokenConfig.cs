namespace GameSalesApi
{
    public class TokenConfig
    {
        public int JWTLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
