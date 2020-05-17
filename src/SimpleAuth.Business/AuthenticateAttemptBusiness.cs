using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class AuthenticateAttemptBusiness : IAuthenticateAttemptBusiness
    {
        private readonly int remainingAttemptsCount;
        private readonly IAuthenticateAttemptData data;

        public AuthenticateAttemptBusiness(int remainingAttemptsCount, IAuthenticateAttemptData data)
        {
            this.remainingAttemptsCount = remainingAttemptsCount;
            this.data = data;
        }

        public async Task<ResponseResult> DecreaseRemainingAttempts(long userId) => await data.DecreaseRemainingAttempts(userId);

        public async Task<int> GetRemainingAttemptCount(long userId) => await data.GetRemainingAttemptCount(remainingAttemptsCount, userId);

        public async Task ResetRemainingAttempts(long userId) => await data.ResetRemainingAttempts(userId);
    }
}
