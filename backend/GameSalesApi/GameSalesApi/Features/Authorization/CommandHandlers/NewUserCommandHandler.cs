﻿using DataAccess;
using GameSalesApi.Features.Authorization.Commands;
using GameSalesApi.Helpers;
using Infrastructure.HandlerBase;
using Infrastructure.Results;
using Model;
using System;
using System.Linq;

namespace GameSalesApi.Features.Authorization.CommandHandlers
{
    /// <summary>
    /// Command handler for creating new user
    /// </summary>
    public class NewUserCommandHandler : CommandHandlerDecoratorBase<NewUserCommand, Result>
    {
        private readonly GameSalesContext _rDBContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="dbContext"><see cref="GameSalesContext"/></param>
        public NewUserCommandHandler(GameSalesContext dbContext)
            : base(null)
        {
            _rDBContext = dbContext;
        }

        /// <summary>
        /// Creates <see cref="User"/>
        /// </summary>
        /// <param name="command"><see cref="NewUserCommand"/></param>
        public override void Execute(NewUserCommand command)
            => Handle(command);

        /// <summary>
        /// Handles creation of new user
        /// </summary>
        /// <param name="input">Target <see cref="NewUserCommand"/> from controkker</param>
        public override Result Handle(NewUserCommand input)
        {
            var user = _rDBContext.Users.Where(u => u.Email == input.Email).FirstOrDefault();
            if (user != null)
                return Result.Fail($"User with email `{input.Email}` already exist!");

            user = _rDBContext.Users.Where(u => u.Username == input.Username).FirstOrDefault();
            if (user != null)
                return Result.Fail($"User with username `{input.Username}` already exist!");

            if (!PasswordHelpers.IsPasswordSatisfied(input.Password, out string errorMessage))
                return Result.Fail(errorMessage);

            byte[] passwordSalt = PasswordHelpers.GenerateSalt();
            string passwordHash = PasswordHelpers.HashPassword(input.Password, passwordSalt);

            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = input.Email,
                Username = input.Username,
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                PasswordHash = passwordHash,
                FirstName = input.FirstName,
                LastName = input.LastName
            };

            _rDBContext.Users.Add(newUser);

            return Result.Ok();
        }
    }
}
