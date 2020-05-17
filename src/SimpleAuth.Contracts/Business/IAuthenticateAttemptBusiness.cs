using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IAuthenticateAttemptBusiness
    {
        Task<int> GetRemainingAttemptCount(long userId);
        Task ResetRemainingAttempts(long userId);
        Task<ResponseResult> DecreaseRemainingAttempts(long id);
    }
}
