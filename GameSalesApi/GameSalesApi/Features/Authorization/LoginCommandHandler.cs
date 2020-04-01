using DataAccess;
using Infrastructure.CommandBase;
using Infrastructure.Exceptions;
using Infrastructure.HandlerBase;
using Infrastructure.Result;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSalesApi.Features.Authorization
{
    /// <summary>
    /// Command handler for login operation
    /// </summary>
    public class LoginCommandHandler : CommandHandlerDecoratorBase<LoginCommand, Result<TokenDTO>>
    {
        private readonly GameSalesContext _dbContext;
        private readonly TokenCreator _tokenCreator;

        public LoginCommandHandler(GameSalesContext dbContext, TokenCreator jwtCreator)
            : base(null)
        {
            _dbContext = dbContext;
            _tokenCreator = jwtCreator;
        }

        public override void Execute(LoginCommand command)
        {
            throw new InvalidHandlingException();
        }

        /// <summary>
        /// Handles authentication and token generation
        /// </summary>
        public override Result<TokenDTO> Handle(LoginCommand input)
        {
            var user = _dbContext.Users.Where(u => u.Email == input.Email).FirstOrDefault();
            if (user == null)
                return Result.Fail<TokenDTO>($"No such user, Email: {user.Email}");
            if (!PasswordHelpers.ValidatePassword(input.Password, user.PasswordSalt, user.PasswordHash))
                return Result.Fail<TokenDTO>("Invalid credentials");

            var accessToken = _tokenCreator.CreateJWT(user);
            var refreshToken = TokenCreator.GenerateRefreshToken();
            var dbToken = new Token() { 
                RefreshToken = refreshToken, 
                UserId = user.Id, 
                DueDate = DateTime.Now.AddMinutes(_tokenCreator.GetTokenConfig().RefreshTokenLifetime)
            };

            _dbContext.Tokens.Add(dbToken);
            return Result.Ok(new TokenDTO() { AccessToken = accessToken, RefreshToken = refreshToken });
        }
    }
}
