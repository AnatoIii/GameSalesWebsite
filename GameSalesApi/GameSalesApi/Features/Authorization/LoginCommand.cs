using Infrastructure.CommandBase;
using Infrastructure.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSalesApi.Features.Authorization
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
