using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business.Strategies
{
    public interface IAuthenticateAttempsStrategy
    {
        Task<AuthenticateAttempResult> Check(long userId, string hash, string password);
    }
}
