using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GameSalesApi.Features.AccountManagement.Queries;
using Infrastructure.HandlerBase;
using Infrastructure.Results;
using Model;
using DataAccess;

namespace GameSalesApi.Features.AccountManagement.QueryHandlers
{
    /// <summary>
    /// Query handler for get a all users from <see cref="GameSalesContext"/>
    /// </summary>
    public class GetAllUsersQueryHandler
        : QueryHandlerDecoratorBase<GetAllUsersQuery, Result<IEnumerable<User>>>
    {
        private readonly GameSalesContext _rDBContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="userSet">User <see cref="DbSet{User}"/></param>
        public GetAllUsersQueryHandler(GameSalesContext gameSalesContext)
            : base(null)
        {
            _rDBContext = gameSalesContext;
        }

        /// <summary>
        /// Get a certain amount of users from <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="userQuery"><see cref="GetAllUsersQuery"/></param>
        /// <returns><see cref="IEnumerable{User}"/></returns>
        public override Result<IEnumerable<User>> Handle(GetAllUsersQuery userQuery)
        {
            IEnumerable<User> result = _rDBContext.Users.ToList();

            if (result == null)
                return Result.Fail<IEnumerable<User>>("Problem with DB connection");

            return Result.Ok(result);
        }
    }
}
