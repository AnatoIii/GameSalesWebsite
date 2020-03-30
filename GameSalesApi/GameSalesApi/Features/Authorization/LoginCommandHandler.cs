using DataAccess;
using Infrastructure.CommandBase;
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
    public class LoginCommandHandler : CommandHandlerDecoratorBase<LoginCommand, Result<TokenDTO>>
    {
        private readonly GameSalesContext dbContext;
        private TokenConfig tokenConfig;
        public LoginCommandHandler(GameSalesContext dbContext, IOptions<TokenConfig> tokenConfig)
            : base(null)
        {
            this.dbContext = dbContext;
            this.tokenConfig = tokenConfig.Value;
        }

        public override void Execute(LoginCommand command)
        {
            throw new NotImplementedException();
        }

        public override Result<TokenDTO> Handle(LoginCommand input)
        {
            Result<TokenDTO> result;
            var user = dbContext.Users.Where(u => u.Email == input.Email).FirstOrDefault();
            if (user == null)
            {
                result = Result.Fail<TokenDTO>($"No such user, Email: {user.Email}");
                return result;
            }
            if (!PasswordHelpers.ValidatePassword(input.Password, user.PasswordSalt, user.PasswordHash))
            {
                result = Result.Fail<TokenDTO>("Invalid credentials");
                return result;
            }

            var token = TokenHelperFunctions.CreateJWT(user, tokenConfig);
            string refreshToken = TokenHelperFunctions.GenerateRefreshToken();
            var dbToken = new Token() { RefreshToken = refreshToken, UserId = user.Id, DueDate = DateTime.Now.AddMinutes(tokenConfig.RefreshTokenLifetime) };

            dbContext.Tokens.Add(dbToken);

            result = Result.Ok<TokenDTO>(new TokenDTO() { AccessToken = token, RefreshToken = refreshToken });
            return result;
        }
    }
}
