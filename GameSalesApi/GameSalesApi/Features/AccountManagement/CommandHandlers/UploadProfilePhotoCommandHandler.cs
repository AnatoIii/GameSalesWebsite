using DataAccess;
using GameSalesApi.Features.AccountManagement.Commands;
using GameSalesApi.Helpers;
using Infrastructure.HandlerBase;
using Infrastructure.Result;
using Model;
using System;

namespace GameSalesApi.Features.AccountManagement.CommandHandlers
{
    public class UploadProfilePhotoCommandHandler
        : CommandHandlerDecoratorBase<UploadProfilePhotoCommand, Result>
    {
        private readonly GameSalesContext _rDBContext;
        private readonly ImageService _imageService;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="dbContext"><see cref="GameSalesApi"/></param>
        /// <param name="imageService"><see cref="ImageService"/></param>
        public UploadProfilePhotoCommandHandler(GameSalesContext dbContext, ImageService imageService)
            : base(null)
        {
            _rDBContext = dbContext;
            _imageService = imageService;
        }

        /// <summary>
        /// Update <see cref="User"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="command"><see cref="UploadProfilePhotoCommand"/></param>
        public override void Execute(UploadProfilePhotoCommand command)
            => Handle(command);

        /// <summary>
        /// Update <see cref="User"/> in <see cref="GameSalesContext"/>
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

            string photoLink = null;

            if (input.Image != null)
            {
                var uploadedImage = _imageService.UploadImage(input.Image).Result;
                photoLink = uploadedImage.Link;
            }

            NewUser newUser = new NewUser()
            {
                Id = input.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                NotificationViaEmail = user.NotificationViaEmail,
                NotificationViaTelegram = user.NotificationViaTelegram,
                PhotoLink = photoLink ?? user.PhotoLink,
                PasswordSalt = user.PasswordSalt,
                PasswordHash = user.PasswordHash
            };

            user.UpdateUser(newUser);

            _rDBContext.Users.Update(user);

            return Result.Ok(user.PhotoLink);
        }
    }
}
