using Infrastructure.CommandBase;
using Infrastructure.Result;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameSalesApi.Features.AccountManagement
{
    /// <summary>
    /// Model for <see cref="AccountController.UpdateEmail(UpdateUserEmail)"/>
    /// </summary>
    public class UpdateUserEmail : ICommand<Result>
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Required]
        public string Email { get; set; }
    }
}
