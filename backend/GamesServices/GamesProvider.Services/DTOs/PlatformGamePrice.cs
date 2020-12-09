namespace GamesProvider.Services.DTOs
{
    public class PlatformGamePrice
    {
        public PlatformDTO Platform { get; set; }
        public int BasePrice { get; set; }
        public int DiscountedPrice { get; set; }
        public int CurrencyId { get; set; }
        public string GameURL { get; set; }
    }
}
