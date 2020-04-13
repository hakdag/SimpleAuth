using SimpleAuth.Common;

namespace SimpleAuth.Contracts.Business
{
    public interface IAuthorizationService
    {
        AuthorizationResult ValidateToken(string token, string secret);
    }
}
