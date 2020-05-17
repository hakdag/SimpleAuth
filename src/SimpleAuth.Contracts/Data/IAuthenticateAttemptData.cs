using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IAuthenticateAttemptData
    {
        Task<int> GetRemainingAttemptCount(int remainingAttemptsCount, long userId);
        Task ResetRemainingAttempts(long userId);
        Task<ResponseResult> DecreaseRemainingAttempts(long userId);
    }
}
