using Infrastructure.HandlerBase;
using Infrastructure.Result;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace GameSalesApi.Features.AccountManagement
{
    /// <summary>
    /// Query handler for get a certain amount of users from <see cref="GameSalesContext"/>
    /// </summary>
    public class GetAllUsersByCountQueryHandler
        : QueryHandlerDecoratorBase<GetUser, Result<IEnumerable<User>>>
    {
        private readonly DbSet<User> _rUserSet;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="userSet">User <see cref="DbSet{User}"/></param>
        public GetAllUsersByCountQueryHandler(DbSet<User> userSet)
            : base(null)
        {
            _rUserSet = userSet;
        }

        /// <summary>
        /// Get a certain amount of users from <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="userQuery"><see cref="GetUser"/></param>
        /// <returns><see cref="IEnumerable{User}"/></returns>
        public override Result<IEnumerable<User>> Handle(GetUser userQuery)
        {
            var usersCount = userQuery.UsersMaxCount;
            IEnumerable<User> result = _rUserSet.ToListAsync().Result;
            if (usersCount > 0)
                result = result.Take(usersCount);

            return Result.Ok(result);
        }
    }
}
