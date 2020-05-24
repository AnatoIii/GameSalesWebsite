using Microsoft.AspNetCore.Http;

namespace GameSalesApi.Features.Authorization
{
    public class ImageDTO
    {
        public IFormFile Image { get; set; }
    }
}
