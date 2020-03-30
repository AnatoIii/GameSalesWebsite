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
    public class RefreshTokenCommandHandler : CommandHandlerDecoratorBase<TokenDTO, Result<TokenDTO>>
    {
        private readonly GameSalesContext dbContext;
        private TokenConfig tokenConfig;
        public RefreshTokenCommandHandler(GameSalesContext dbContext, IOptions<TokenConfig> tokenConfig) : base(null)
        {
            this.dbContext = dbContext;
            this.tokenConfig = tokenConfig.Value;
        }

        public override void Execute(TokenDTO command)
        {
            throw new NotImplementedException();
        }

        public override Result<TokenDTO> Handle(TokenDTO input)
        {
            Result<TokenDTO> result;
            var userId = TokenHelperFunctions.GetUserId(input);
            if (userId == null)
            {
                result = Result.Fail<TokenDTO>($"No such user, userId: {userId}");
                return result;
            }

            var user = dbContext.Users.Where(u => u.Id == userId).FirstOrDefault();
            var refreshToken = dbContext.Tokens.Where(t => t.UserId == userId).FirstOrDefault();
            if(refreshToken == null)
            {
                result = Result.Fail<TokenDTO>($"No refresh token with such userId: {userId}");
            }

            dbContext.Tokens.Remove(refreshToken);
            var accessToken = TokenHelperFunctions.CreateJWT(user, tokenConfig);
            string refreshTokenValue = TokenHelperFunctions.GenerateRefreshToken();
            var dbToken = new Token() { RefreshToken = refreshTokenValue, UserId = user.Id, DueDate = DateTime.Now.AddMinutes(tokenConfig.RefreshTokenLifetime) };
            dbContext.Tokens.Add(dbToken);

            result = Result.Ok<TokenDTO>(new TokenDTO() { AccessToken = accessToken, RefreshToken = refreshTokenValue });
            return result;
        }
    }
}
