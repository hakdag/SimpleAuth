﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SimpleAuth.Business
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings appSettings;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserService userService;

        public AuthenticationService(
            IOptions<AppSettings> appSettings,
            IPasswordHasher passwordHasher,
            IUserService userService)
        {
            this.appSettings = appSettings.Value;
            this.passwordHasher = passwordHasher;
            this.userService = userService;
        }

        public AuthenticationToken Authenticate(string username, string password)
        {
            var user = userService.GetByUserName(username);
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
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
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

            return authenticationToken;
        }
    }
}
