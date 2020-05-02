using System;
using DataAccess;
using GameSalesApi.Features.Authorization;
using GameSalesApi.Features.AccountManagement.Commands;
using Infrastructure.HandlerBase;
using Infrastructure.Result;
using Model;

namespace GameSalesApi.Features.AccountManagement.CommandHandlers
{

    /// <summary>
    /// Command handler for remove <see cref="User"/> from <see cref="GameSalesContext"/>
    /// </summary>
    public class RemoveUserCommandHandler
        : CommandHandlerDecoratorBase<RemoveUserCommand, Result>
    {
        private readonly GameSalesContext _rDBContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="dbContext"><see cref="GameSalesApi"/></param>
        public RemoveUserCommandHandler(GameSalesContext dbContext)
            : base(null)
        {
            _rDBContext = dbContext;
        }

        /// <summary>
        /// Remove <see cref="User"/> by <see cref="RemoveUserCommand.UserId"/>
        /// </summary>
        /// <param name="command"><see cref="RemoveUserCommand"/></param>
        public override void Execute(RemoveUserCommand command)
            => Handle(command);

        /// <summary>
        /// Remove <see cref="User"/> by <see cref="RemoveUserCommand.UserId"/>
        /// </summary>
        /// <param name="input"><see cref="RemoveUserCommand"/></param>
        /// <returns><see cref="Result"/></returns>
        public override Result Handle(RemoveUserCommand input)
        {
            if (input.UserId == null)
                throw new ArgumentNullException(nameof(input.UserId));

            var user = _rDBContext.Users.FindAsync(input.UserId).Result;

            if (user == null)
                return Result.Fail($"User with id {input.UserId} not found!");

            _rDBContext.Users.Remove(user);

            return Result.Ok();
        }
    }
}
