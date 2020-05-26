using Infrastructure.CommandBase;
using Infrastructure.Result;
using Microsoft.AspNetCore.Http;
using System;


namespace GameSalesApi.Features.AccountManagement.Commands
{
    /// <summary>
    /// Model for <see cref="AccountController.UploadProfilePhoto(UploadProfilePhotoCommand)"/>
    /// </summary>
    public class UploadProfilePhotoCommand: ICommand<Result>
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// User profile photo
        /// </summary>
        public IFormFile Image { get; set; }
    }
}
