using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameSalesApi.Features.AccountManagement
{
    /// <summary>
    /// A service, responsible for managing user images
    /// </summary>
    public class ImageService
    {
        private readonly ImgurConfig _imgurConfig;
        private readonly HttpClient _client;
        public ImageService(IOptions<ImgurConfig> options)
        {
            _imgurConfig = options.Value;
            _client = new HttpClient();
        }

        public async Task<IImage> UploadImage(IFormFile image)
        {
            var client = new ImgurClient(_imgurConfig.ClientId);
            var endpoint = new ImageEndpoint(client);
            IImage uploadedImage = await endpoint.UploadImageStreamAsync(image.OpenReadStream());
            return uploadedImage;
        }
    }
}
