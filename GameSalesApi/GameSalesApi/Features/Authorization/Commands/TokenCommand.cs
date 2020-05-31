using Infrastructure.CommandBase;
using Infrastructure.Results;

namespace GameSalesApi.Features.Authorization.Commands
{
    /// <summary>
    /// Model for auth commands in <see cref="AuthController"/>
    /// </summary>
    public class TokenCommand : ICommand<Result<TokenDTO>>
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
