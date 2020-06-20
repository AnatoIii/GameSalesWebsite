using System;
using DataAccess;
using GameSalesApi.Features.AccountManagement.Queries;
using Infrastructure.HandlerBase;
using Infrastructure.Results;
using Model;

namespace GameSalesApi.Features.AccountManagement.QueryHandlers
{
    /// <summary>
    /// Query handler for get <see cref="User"/> by <see cref="User.Id"/> from <see cref="GameSalesContext"/>
    /// </summary>
    public class GetUserQueryHandler 
        : QueryHandlerDecoratorBase<GetUserQuery, Result<User>>
    {
        private readonly GameSalesContext _rDBContext;
        private static readonly Guid _prEmptyGuid = Guid.Empty;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="gameSalesContext"><see cref="GameSalesContext"/></param>
        public GetUserQueryHandler(GameSalesContext gameSalesContext)
            : base(null)
        {
            _rDBContext = gameSalesContext;
        }

        /// <summary>
        /// Get a <see cref="User"/> by <see cref="User.Id"/>
        /// </summary>
        /// <param name="input"><see cref="GetUserQuery"/></param>
        /// <returns><see cref="Result{User}"/></returns>
        public override Result<User> Handle(GetUserQuery input)
        {
            if (input.UserId == _prEmptyGuid)
                return Result.Fail<User>($"User not found. User can`t be with empty id.");

            var user = _rDBContext.Users.FindAsync(input.UserId).Result;

            if (user == null)
                return Result.Fail<User>($"User not found. User with id {input.UserId} not found.");

            return Result.Ok(user);
        }
    }
}
