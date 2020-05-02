using Infrastructure.CommandBase;
using Infrastructure.Result;

namespace GameSalesApi.Features.Authorization.Commands
{
    /// <summary>
    /// Transfer object for login Operation <see cref="AuthController.Login(LoginCommand)"/>
    /// </summary>
    public class LoginCommand : ICommand<Result<TokenDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
