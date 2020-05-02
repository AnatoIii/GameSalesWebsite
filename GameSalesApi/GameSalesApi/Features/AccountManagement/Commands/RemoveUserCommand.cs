using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.CommandBase;
using Infrastructure.Result;

namespace GameSalesApi.Features.AccountManagement.Commands
{
    /// <summary>
    /// Model for <see cref="AccountController.UpdateUser(UpdateUserCommand)"/>
    /// </summary>
    public class RemoveUserCommand : ICommand<Result>
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }
    }
}
