using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameSalesApi.Features.Authorization
{
    public class TokenCreator
    {
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenConfig _tokenConfig;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenConfig GetTokenConfig() => _tokenConfig;

        public TokenCreator(IOptions<TokenConfig> options)
        {
            _tokenConfig = options.Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret));
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateJWT(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _tokenConfig.Issuer,
                audience: _tokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenConfig.JWTLifetime),
                signingCredentials: _signingCredentials
            );

            return _tokenHandler.WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static Guid GetUserId(TokenDTO tokenDTO)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenDTO.AccessToken);
            return Guid.Parse(token.Id);
        }
    }
}
