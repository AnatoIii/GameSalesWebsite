using Infrastructure.CommandBase;
using Infrastructure.Results;

namespace GameSalesApi.Features.Authorization.Commands
{
    /// <summary>
    /// Model for login operation <see cref="AuthController.Login(LoginCommand)"/>
    /// </summary>
    public class LoginCommand : ICommand<Result<TokenDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
