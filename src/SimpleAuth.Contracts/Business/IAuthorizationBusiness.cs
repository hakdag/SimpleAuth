using SimpleAuth.Common;

namespace SimpleAuth.Contracts.Business
{
    public interface IAuthorizationBusiness
    {
        AuthorizationResult ValidateToken(string token);
    }
}
