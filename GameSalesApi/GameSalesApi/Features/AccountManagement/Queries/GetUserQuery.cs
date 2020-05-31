using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.CommandBase;
using Infrastructure.Results;
using Model;

namespace GameSalesApi.Features.AccountManagement.Queries
{
    /// <summary>
    /// Model for get methods from <see cref="AccountController.GetUser(GetUserQuery)"/>
    /// </summary>
    public class GetUserQuery : IQuery<Result<User>>
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }
    }
}
