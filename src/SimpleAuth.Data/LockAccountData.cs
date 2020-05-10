using Dapper;
using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class LockAccountData : BaseData<UserAccountLock>, ILockAccountData
    {
        public readonly string ErrorMessage_LockError = "Error occured when locking the user account.";
        public readonly string ErrorMessage_UnLockError = "Error occured when unlocking the user account.";
        public readonly string WarningMessage_AccountLocked = "Account is locked.";

        public LockAccountData(IRepository repository) : base(repository) { }

        public async Task<ResponseResult> CheckUser(long userId)
        {
            var result = await Repository.Connection.QueryFirstOrDefaultAsync<int>("select count(userid) from public.useraccountlock where userid = @userId", new { userId });
            if (result == 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { WarningMessage_AccountLocked } };
        }

        public async Task<ResponseResult> LockAccount(long userId)
        {
            var updatedDate = DateTime.Now;
            var result = await Execute("INSERT INTO public.useraccountlock(userid) VALUES(@userId) ON CONFLICT (userid) DO UPDATE SET updateddate = @updatedDate", new { userId, updatedDate });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_LockError } };
        }

        public async Task<ResponseResult> UnLockAccount(long userId)
        {
            var result = await Execute("DELETE FROM public.useraccountlock where userid = @userId", new { userId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UnLockError } };
        }
    }
}
