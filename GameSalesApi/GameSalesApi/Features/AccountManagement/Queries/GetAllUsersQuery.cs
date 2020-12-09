using System.Collections.Generic;
using Infrastructure.CommandBase;
using Infrastructure.Results;
using Model;

namespace GameSalesApi.Features.AccountManagement.Queries
{
    /// <summary>
    /// Model for get methods from <see cref="AccountController.GetAll"/>
    /// </summary>
    public class GetAllUsersQuery : IQuery<Result<IEnumerable<User>>>
    { }
}
