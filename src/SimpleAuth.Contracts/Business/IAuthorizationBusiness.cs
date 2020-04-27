using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IAuthorizationBusiness
    {
        Task<AuthorizationResult> ValidateToken(string token);
    }
}
