using System;
using DataAccess;
using GameSalesApi.Features.AccountManagement.Commands;
using GameSalesApi.Helpers;
using Infrastructure.HandlerBase;
using Infrastructure.Result;
using Model;

namespace GameSalesApi.Features.AccountManagement.CommandHandlers
{
    /// <summary>
    /// Command handler for update <see cref="User"/> in <see cref="GameSalesContext"/>
    /// </summary>
    public class UpdateUserCommandHandler
        : CommandHandlerDecoratorBase<UpdateUserCommand, Result>
    {
        private readonly GameSalesContext _rDBContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="dbContext"><see cref="GameSalesApi"/></param>
        public UpdateUserCommandHandler(GameSalesContext dbContext)
            : base(null)
        {
            _rDBContext = dbContext;
        }

        /// <summary>
        /// Update <see cref="User"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="command"><see cref="UpdateUserCommand"/></param>
        public override void Execute(UpdateUserCommand command)
            => Handle(command);
        
        /// <summary>
        /// Update <see cref="User"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="input"><see cref="UpdateUserCommand"/></param>
        /// <returns><see cref="Result"/></returns>
        public override Result Handle(UpdateUserCommand input)
        {
            if (input.UserId == null)
                throw new ArgumentNullException(nameof(input.UserId));

            var user = _rDBContext.Users.FindAsync(input.UserId).Result;

            if (user == null)
                return Result.Fail($"User with id {input.UserId} not found!");

            byte[] passwordSalt = null;
            string passwordHash = null;

            if (!string.IsNullOrWhiteSpace(input.Password))
            {
                if (!PasswordHelpers.IsPasswordSatisfied(input.Password, out string errorMessage))
                    return Result.Fail(errorMessage);

                passwordSalt = PasswordHelpers.GenerateSalt();
                passwordHash = PasswordHelpers.HashPassword(input.Password, passwordSalt);
            }

            NewUser newUser = new NewUser()
            {
                Id = input.UserId,
                FirstName = input.FirstName ?? user.FirstName,
                LastName = input.LastName ?? user.LastName,
                Email = input.Email ?? user.Email,
                Username = input.Username ?? user.Username,
                NotificationViaEmail = input.NotificationViaEmail ?? user.NotificationViaEmail,
                NotificationViaTelegram = input.NotificationViaTelegram ?? user.NotificationViaTelegram,
                PhotoLink = input.PhotoLink ?? user.PhotoLink,
                PasswordSalt = passwordSalt != null ? Convert.ToBase64String(passwordSalt) : user.PasswordSalt,
                PasswordHash = passwordHash ?? user.PasswordHash
            };

            user.UpdateUser(newUser);

            _rDBContext.Users.Update(user);

            return Result.Ok();
        }
    }
}
