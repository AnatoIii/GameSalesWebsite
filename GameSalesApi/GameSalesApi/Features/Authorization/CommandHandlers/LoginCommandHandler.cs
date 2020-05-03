using System.Linq;
using DataAccess;
using GameSalesApi.Features.Authorization.Commands;
using GameSalesApi.Helpers;
using Infrastructure.HandlerBase;
using Infrastructure.Result;

namespace GameSalesApi.Features.Authorization.CommandHandlers
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
            => Handle(command);

        /// <summary>
        /// Handles authentication and token generation
        /// </summary>
        public override Result<TokenDTO> Handle(LoginCommand input)
        {
            var user = _dbContext.Users.Where(u => u.Email == input.Email).FirstOrDefault();

            if (user == null)
                return Result.Fail<TokenDTO>($"No such user, Email: {input.Email}");

            if (!PasswordHelpers.ValidatePassword(input.Password, user.PasswordSalt, user.PasswordHash))
                return Result.Fail<TokenDTO>("Incorrect password");

            return Result.Ok(_tokenCreator.CreateDTOToken(user,_dbContext));
        }
    }
}
