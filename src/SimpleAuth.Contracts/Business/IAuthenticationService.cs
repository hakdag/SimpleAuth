using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IAuthenticationService
    {
        Task<AuthenticationToken> Authenticate(string username, string password);
    }
}
