using System.Linq;
using DataAccess;
using Infrastructure.Exceptions;
using Infrastructure.HandlerBase;
using Infrastructure.Result;

namespace GameSalesApi.Features.Authorization
{
    /// <summary>
    /// Command handler for refreshing user tokens
    /// </summary>
    public class RefreshTokenCommandHandler : CommandHandlerDecoratorBase<TokenDTO, Result<TokenDTO>>
    {
        private readonly GameSalesContext _dbContext;
        private readonly TokenCreator _tokenCreator;

        public RefreshTokenCommandHandler(GameSalesContext dbContext, TokenCreator tokenCreator) : base(null)
        {
            _dbContext = dbContext;
            _tokenCreator = tokenCreator;
        }

        public override void Execute(TokenDTO command)
        {
            throw new InvalidHandlingException();
        }

        /// <summary>
        /// Handles revoking and generating tokens
        /// </summary>
        /// <param name="tokenDTO"><see cref="TokenDTO"/></param>
        public override Result<TokenDTO> Handle(TokenDTO tokenDTO)
        {
            var userId = TokenCreator.GetUserId(tokenDTO);
            if (userId == null)
            {
                return Result.Fail<TokenDTO>($"No such user, userId: {userId}");
            }  

            var user = _dbContext.Users.Where(u => u.Id == userId).FirstOrDefault();
            var refreshToken = _dbContext.Tokens.Where(t => t.RefreshToken == tokenDTO.RefreshToken).FirstOrDefault();
            if(refreshToken == null)
            {
                return Result.Fail<TokenDTO>($"No refresh token with such value: {tokenDTO.RefreshToken}");
            }
            _dbContext.Tokens.Remove(refreshToken);

            return Result.Ok(_tokenCreator.CreateDTOToken(user,_dbContext));
        }
    }
}
