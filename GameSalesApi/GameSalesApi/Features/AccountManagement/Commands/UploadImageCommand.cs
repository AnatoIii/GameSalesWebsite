using Infrastructure.CommandBase;
using Infrastructure.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameSalesApi.Features.AccountManagement.Commands
{
    /// <summary>
    /// Model for <see cref="CommandHandlers.UploadProfilePhotoCommandHandler.Handle(UploadProfilePhotoCommand)"/>
    /// </summary>
    public class UploadImageCommand : ICommand<Result<string>>
    {
        /// <summary>
        /// Image for upload
        /// </summary>
        [Required]
        public IFormFile Image { get; set; }
    }
}
