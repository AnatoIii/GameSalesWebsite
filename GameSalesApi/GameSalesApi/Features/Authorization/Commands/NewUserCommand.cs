using System.ComponentModel.DataAnnotations;
using Infrastructure.CommandBase;
using Infrastructure.Result;

namespace GameSalesApi.Features.Authorization.Commands
{
    /// <summary>
    /// Model for <see cref="AuthController.CreateNewUser(NewUserCommand)"/>
    /// </summary>
    public class NewUserCommand : ICommand<Result>
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Second name
        /// </summary>
        public string LastName { get; set; }
    }
}
