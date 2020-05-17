using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class PasswordHistoryData : BaseData<PasswordHistory>, IPasswordHistoryData
    {
        public readonly string ErrorMessage_PasswordHistoryGenericError = "Error occured when adding password to the history.";

        public PasswordHistoryData(IRepository repository) : base(repository) { }

        public async Task<IEnumerable<PasswordHistory>> GetHistory(long userId, int passwordChangeHistoryRule)
        {
            var items = await RunQuery("SELECT passwordhash FROM public.passwordhistory where userid = @userId order by createddate desc limit @passwordChangeHistoryRule", new { userId, passwordChangeHistoryRule });
            return items;
        }

        public async Task<ResponseResult> Create(long userId, string passwordHash)
        {
            var result = await Execute("INSERT INTO public.passwordhistory(userId,passwordHash) VALUES(@userId, @passwordHash)", new { userId, passwordHash });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_PasswordHistoryGenericError } };
        }
    }
}
