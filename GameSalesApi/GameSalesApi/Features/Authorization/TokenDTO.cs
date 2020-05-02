using Infrastructure.CommandBase;
using Infrastructure.Result;

namespace GameSalesApi.Features.Authorization
{
    /// <summary>
    /// Transfer object for auth <see cref="AuthController"/>
    /// </summary>
    public class TokenDTO : ICommand<Result<TokenDTO>>
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
