using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DataAccess;
using Model;

namespace GameSalesApi.Features.Authorization
{
    /// <summary>
    /// A service, responsible for managing token actions
    /// </summary>
    public class TokenCreator
    {
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenConfig _tokenConfig;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenCreator(IOptions<TokenConfig> options)
        {
            _tokenConfig = options.Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret));
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// Creates a jwt access token for the given user
        /// </summary>
        private string CreateJWT(User user)
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

        /// <summary>
        /// Helper function for generating a refresh token
        /// </summary>
        private static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Helper function for getting UserId from token
        /// </summary>
        public static Guid GetUserId(TokenDTO tokenDTO)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenDTO.AccessToken);
            return Guid.Parse(token.Id);
        }

        public TokenDTO CreateDTOToken(User user, GameSalesContext dbContext)
        {
            var accessToken = CreateJWT(user);
            var refreshTokenValue = GenerateRefreshToken();
            var dbToken = new Token()
            {
                RefreshToken = refreshTokenValue,
                UserId = user.Id,
                DueDate = DateTime.Now.AddMinutes(_tokenConfig.RefreshTokenLifetime)
            };
            dbContext.Tokens.Add(dbToken);
            return new TokenDTO() { AccessToken = accessToken, RefreshToken = refreshTokenValue };
        }
    }
}
