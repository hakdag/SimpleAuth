using Dapper;
using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class AuthenticateAttemptData : BaseData<AuthenticateAttempt>, IAuthenticateAttemptData
    {
        public readonly string ErrorMessage_DecreaseRemainingAttempt = "Error occured when inserting authenticate attempt.";

        public AuthenticateAttemptData(IRepository repository) : base(repository) { }

        public async Task<ResponseResult> DecreaseRemainingAttempts(long userId)
        {
            var result = await Execute("INSERT INTO public.authenticateattempt(userid) VALUES(@userId)", new { userId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_DecreaseRemainingAttempt } };
        }

        public async Task<int> GetRemainingAttemptCount(int remainingAttemptsCount, long userId)
        {
            var result = await Repository.Connection.QueryFirstOrDefaultAsync<int>("select count(userid) from public.authenticateattempt where isdeleted = false and userid = @userId", new { userId });
            return remainingAttemptsCount - result;
        }

        public async Task ResetRemainingAttempts(long userId) => await Execute("UPDATE public.authenticateattempt SET isdeleted = true WHERE userid = @userId", new { userId });
    }
}
