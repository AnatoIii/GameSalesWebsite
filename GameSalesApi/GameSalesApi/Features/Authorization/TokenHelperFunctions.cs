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
    public static class TokenHelperFunctions
    {
        public static string CreateJWT(User user, TokenConfig tokenConfig)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim("UserID",user.Id.ToString()),
                new Claim("Role",user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://localhost:5000",
                audience: "*",
                claims: claims,
                expires: DateTime.Now.AddMinutes(tokenConfig.JWTLifetime),
                signingCredentials: credentials
            );
            return handler.WriteToken(token);
        }
        public static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static Guid GetUserId(TokenDTO tokenDTO)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenDTO.AccessToken);
            return Guid.Parse(token.Claims.Where(c => c.Type == "UserID").FirstOrDefault().Value);
        }
    }
}
