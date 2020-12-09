using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.CommandBase;
using Infrastructure.Results;

namespace GameSalesApi.Features.AccountManagement.Commands
{
    /// <summary>
    /// Model for <see cref="AccountController.RemoveUser(RemoveUserCommand)"/>
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
