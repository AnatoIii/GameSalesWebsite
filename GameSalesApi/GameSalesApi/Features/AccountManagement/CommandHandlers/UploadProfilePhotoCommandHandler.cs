using DataAccess;
using GameSalesApi.Features.AccountManagement.Commands;
using GameSalesApi.Helpers;
using Infrastructure.DecoratorsFactory;
using Infrastructure.HandlerBase;
using Infrastructure.Results;
using Microsoft.Extensions.Options;
using Model;
using System;

namespace GameSalesApi.Features.AccountManagement.CommandHandlers
{
    /// <summary>
    /// Command handler for update user's profile photo <see cref="User.PhotoLink"/> in <see cref="GameSalesContext"/>
    /// </summary>
    public class UploadProfilePhotoCommandHandler
        : CommandHandlerDecoratorBase<UploadProfilePhotoCommand, Result>
    {
        private readonly GameSalesContext _rDBContext;
        private readonly ImgurConfig _imgurConfig;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="dbContext"><see cref="GameSalesApi"/></param>
        /// <param name="options"><see cref="ImgurConfig"/></param>
        public UploadProfilePhotoCommandHandler(GameSalesContext dbContext, ImgurConfig options)
            : base(null)
        {
            _rDBContext = dbContext;
            _imgurConfig = options;
        }

        /// <summary>
        /// Update user's profile photo <see cref="User.PhotoLink"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="command"><see cref="UploadProfilePhotoCommand"/></param>
        public override void Execute(UploadProfilePhotoCommand command)
            => Handle(command);

        /// <summary>
        /// Update user's profile photo <see cref="User.PhotoLink"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="input"><see cref="UpdateUserCommand"/></param>
        /// <returns><see cref="Result"/></returns>
        public override Result Handle(UploadProfilePhotoCommand input)
        {
            if (input.UserId == null)
                throw new ArgumentNullException(nameof(input.UserId));

            var user = _rDBContext.Users.FindAsync(input.UserId).Result;

            if (user == null)
                return Result.Fail($"User with id {input.UserId} not found!");

            var uploadImageCommand = new UploadImageCommand { Image = input.Image };

            var handler = new CommandDecoratorBuilder<UploadImageCommand, Result<string>>()
                .Add<UploadImageCommandHandler>()
                    .AddParameter<ImgurConfig>(_imgurConfig)
                .Build();

            user.PhotoLink = handler.Handle(uploadImageCommand).Value;

            _rDBContext.Users.Update(user);

            return Result.Ok(user.PhotoLink);
        }
    }
}
