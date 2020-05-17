using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Business;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SimpleAuth.Business
{
    public class TokenGenerationBusiness : ITokenGenerationBusiness
    {
        public AuthenticationToken Generate(User user, string secret)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            var authenticationToken = new AuthenticationToken
            {
                token = token,
                username = user.UserName,
                expires = tokenDescriptor.Expires.Value
            };
            return authenticationToken;
        }
    }
}
