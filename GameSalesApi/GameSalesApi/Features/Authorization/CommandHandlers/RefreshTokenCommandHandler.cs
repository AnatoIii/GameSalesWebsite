using System.Linq;
using DataAccess;
using GameSalesApi.Features.Authorization.Commands;
using Infrastructure.Exceptions;
using Infrastructure.HandlerBase;
using Infrastructure.Result;

namespace GameSalesApi.Features.Authorization.CommandHandlers
{
    /// <summary>
    /// Command handler for refreshing user tokens
    /// </summary>
    public class RefreshTokenCommandHandler : CommandHandlerDecoratorBase<TokenCommand, Result<TokenDTO>>
    {
        private readonly GameSalesContext _dbContext;
        private readonly TokenCreator _tokenCreator;

        public RefreshTokenCommandHandler(GameSalesContext dbContext, TokenCreator tokenCreator) : base(null)
        {
            _dbContext = dbContext;
            _tokenCreator = tokenCreator;
        }

        public override void Execute(TokenCommand command)
        {
            throw new InvalidHandlingException();
        }

        /// <summary>
        /// Handles revoking and generating tokens
        /// </summary>
        /// <param name="tokenDTO"><see cref="TokenDTO"/></param>
        public override Result<TokenDTO> Handle(TokenCommand tokenCommand)
        {
            var userId = TokenCreator.GetUserId(tokenCommand);

            if (userId == null)
                return Result.Fail<TokenDTO>($"Id can`t be null!");

            var user = _dbContext.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (user == null)
                return Result.Fail<TokenDTO>($"User with id {userId} not found!");

            var refreshToken = _dbContext.Tokens.Where(t => t.RefreshToken == tokenCommand.RefreshToken).FirstOrDefault();

            if(refreshToken == null)
                return Result.Fail<TokenDTO>($"No refresh token with such value: {tokenCommand.RefreshToken}");

            _dbContext.Tokens.Remove(refreshToken);

            return Result.Ok(_tokenCreator.CreateDTOToken(user,_dbContext));
        }
    }
}
