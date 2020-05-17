using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class LockAccountBusiness : ILockAccountBusiness
    {
        private readonly ILockAccountData data;
        private readonly IAuthenticateAttemptBusiness authenticateAttemptBusiness;
        private readonly IUserBusiness userBusiness;

        public readonly string ErrorMessage_UserDoesNotExist = "User does not exist.";

        public LockAccountBusiness(
            ILockAccountData data,
            IAuthenticateAttemptBusiness authenticateAttemptBusiness,
            IUserBusiness userBusiness)
        {
            this.data = data;
            this.authenticateAttemptBusiness = authenticateAttemptBusiness;
            this.userBusiness = userBusiness;
        }

        public async Task<ResponseResult> LockAccount(long userId)
        {
            // check if userId exists
            var user = await userBusiness.Get(userId);
            if (user == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesNotExist } };
            }

            return await data.LockAccount(userId);
        }

        public async Task<ResponseResult> UnLockAccount(long userId)
        {
            // check if userId exists
            var user = await userBusiness.Get(userId);
            if (user == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesNotExist } };
            }

            await authenticateAttemptBusiness.ResetRemainingAttempts(userId);

            return await data.UnLockAccount(userId);
        }

        public async Task<ResponseResult> CheckUser(long userId)
        {
            // check if userId exists
            var user = await userBusiness.Get(userId);
            if (user == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesNotExist } };
            }

            return await data.CheckUser(userId);
        }
    }
}
