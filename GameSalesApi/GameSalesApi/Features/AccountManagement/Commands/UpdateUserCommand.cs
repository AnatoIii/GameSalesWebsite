using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.CommandBase;
using Infrastructure.Result;

namespace GameSalesApi.Features.AccountManagement.Commands
{
    /// <summary>
    /// Model for <see cref="AccountController.UpdateUser(UpdateUserCommand)"/>
    /// </summary>
    public class UpdateUserCommand : ICommand<Result>
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// New email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// New username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Current password
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// New first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// New last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Is enabled notifications via email
        /// </summary>
        public bool? NotificationViaEmail { get; set; }

        /// <summary>
        /// Is enabled notifications via telegram
        /// </summary>
        public bool? NotificationViaTelegram { get; set; }
    }
}
