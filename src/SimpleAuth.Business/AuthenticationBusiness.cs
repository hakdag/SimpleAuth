﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly AppSettings appSettings;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserBusiness userService;

        public AuthenticationBusiness(
            IOptions<AppSettings> appSettings,
            IPasswordHasher passwordHasher,
            IUserBusiness userService)
        {
            this.appSettings = appSettings.Value;
            this.passwordHasher = passwordHasher;
            this.userService = userService;
        }

        public async Task<AuthenticationToken> Authenticate(string username, string password)
        {
            var user = await userService.GetByUserName(username);
            if (user == null)
            {
                return null;
            }

            var checkResult = passwordHasher.Check(user.Password, password);
            if (!checkResult.Verified)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            var authenticationToken = new AuthenticationToken
            {
                token = token,
                username = username,
                expires = tokenDescriptor.Expires.Value
            };

            // update user login date and token fields
            await userService.UserLoggedIn(user, token);

            return authenticationToken;
        }
    }
}