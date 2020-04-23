using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace SimpleAuth.Business
{
    public class AuthorizationBusiness : IAuthorizationBusiness
    {
        private readonly TokenValidationParameters tokenValidationParameters;

        public AuthorizationBusiness(TokenValidationParameters tokenValidationParameters)
        {
            this.tokenValidationParameters = tokenValidationParameters;
        }

        public AuthorizationResult ValidateToken(string token)
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
        }
    }
}
