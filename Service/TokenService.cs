using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using IsApi.Configurations;
using IsApi.DTO;
using IsApi.Service.Identity;
using IsApi.Service.Interfaces;

namespace Infrastructure.Identity
{
    public class TokenService : ITokenService
    {
        private JwtSettings _jwtSettings;
        private IUserService _userService;

        public TokenService(JwtSettings jwtSettings, IUserService userService)
        {
            _jwtSettings = jwtSettings;
            _userService = userService;
        }

        public TokenResponse GetToken(TokenRequest request)
        {
            return new TokenResponse(GenerateJwt(request));
        }
        private string GenerateJwt(TokenRequest request) => GenerateEncryptedToken(GetSigningCredentials(), request);

        private string GenerateEncryptedToken(SigningCredentials signingCredentials, TokenRequest request)
        {
            var result = _userService.CheckValidUser(request);
            if (result.Key == false)
            {
                throw new Exception(result.Value);
            }
            var roles = _userService.GetRolesByUsername(request.UserName);
            var claims = new List<Claim>();
            roles.ForEach(x => claims.Add(new Claim("role", x)));
            var user = _userService.FindUserByUsername(request.UserName);
            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            if (!string.IsNullOrEmpty(user.PhoneNumber))
                claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            claims.Add(new Claim(ClaimTypes.Name, user.FullName));
            var token = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature);
        }
    }
}