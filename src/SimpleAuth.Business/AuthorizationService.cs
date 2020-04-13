using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace SimpleAuth.Business
{
    public class AuthorizationService : IAuthorizationService
    {
        public AuthorizationResult ValidateToken(string token, string secret)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(secret))
            {
                return new AuthorizationResult { IsAuthorized = false };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
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

            //var identity = new ClaimsIdentity(claimsPrincipal.Claims, "Bearer");
            //var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            //var ticket = new AuthenticationTicket(principal, "Bearer");
            //return AuthenticateResult.Success(ticket);
        }
    }
}
