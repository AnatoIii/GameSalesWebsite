using GameSalesApi.Features.AccountManagement.Commands;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Infrastructure.HandlerBase;
using Infrastructure.Results;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameSalesApi.Features.AccountManagement.CommandHandlers
{
    /// <summary>
    /// Command handler for uploading images <see cref="User.PhotoLink"/> in <see cref="GameSalesContext"/>
    /// </summary>
    public class UploadImageCommandHandler
        : CommandHandlerDecoratorBase<UploadImageCommand, Result<string>>
    { 
        private readonly ImgurConfig _imgurConfig;
        private readonly HttpClient _client;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="options"><see cref="ImgurConfig"/></param>
        public UploadImageCommandHandler(ImgurConfig options):
            base(null)
        {
            _imgurConfig = options;
            _client = new HttpClient();
        }

        /// <summary>
        /// Upload image for <see cref="User.PhotoLink"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="command"><see cref="UploadProfilePhotoCommand"/></param>
        public override void Execute(UploadImageCommand command)
            => Handle(command);

        /// <summary>
        ///  Upload image for <see cref="User.PhotoLink"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="input"><see cref="UploadImageCommand"/></param>
        /// <returns><see cref="Result"/></returns>
        public override Result<string> Handle(UploadImageCommand input)
        {
            if (input.Image == null)
                throw new ArgumentNullException(nameof(input.Image));
            var client = new ImgurClient(_imgurConfig.ClientId);
            var endpoint = new ImageEndpoint(client);
            return Result.Ok(endpoint.UploadImageStreamAsync(input.Image.OpenReadStream()).Result.Link);
        }
    }
}
