﻿using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class AuthorizationBusiness : IAuthorizationBusiness
    {
        private readonly IUserData userData;
        private readonly TokenValidationParameters tokenValidationParameters;
        private readonly ILockAccountBusiness lockAccountBusiness;

        public AuthorizationBusiness(
            IUserData userData,
            TokenValidationParameters tokenValidationParameters,
            ILockAccountBusiness lockAccountBusiness)
        {
            this.userData = userData;
            this.tokenValidationParameters = tokenValidationParameters;
            this.lockAccountBusiness = lockAccountBusiness;
        }

        public async Task<AuthorizationResult> ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new AuthorizationResult { IsAuthorized = false };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!claimsPrincipal.Identity.IsAuthenticated)
                {
                    return new AuthorizationResult { IsAuthorized = false };
                }

                var usernameClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type.EndsWith("/identity/claims/name"));
                var roleClaims = claimsPrincipal.Claims.Where(c => c.Type.EndsWith("/identity/claims/role"));
                if (usernameClaim == null || roleClaims == null || roleClaims.Count() < 1)
                {
                    return new AuthorizationResult { IsAuthorized = false };
                }

                // check user
                var user = await userData.GetByUserName(usernameClaim.Value);
                if (user == null)
                {
                    return new AuthorizationResult { IsAuthorized = false };
                }

                var lockCheck = await lockAccountBusiness.CheckUser(user.Id);
                if (!lockCheck.Success)
                {
                    return new AuthorizationResult { IsAuthorized = false };
                }

                return new AuthorizationResult
                {
                    IsAuthorized = true,
                    UserName = usernameClaim.Value,
                    Roles = roleClaims.Select(c => c.Value).ToArray()
                };
            }
            catch (SecurityTokenExpiredException exc)
            {
                return new AuthorizationResult { IsAuthorized = false };
            }
            catch (SecurityTokenInvalidSignatureException exc)
            {
                return new AuthorizationResult { IsAuthorized = false };
            }
        }
    }
}
