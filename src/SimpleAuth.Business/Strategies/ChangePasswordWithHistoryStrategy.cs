using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.Strategies;
using SimpleAuth.Contracts.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Business.Strategies
{
    public class ChangePasswordWithHistoryStrategy : IChangePasswordStrategy
    {
        private readonly IChangePasswordData changePasswordData;
        private readonly IPasswordHistoryBusiness passwordHistoryBusiness;
        private readonly IPasswordHasher passwordHasher;

        public readonly string ErrorMessage_PasswordCheckRule = "Your password cannot be same as your last {0} of your passwords.";

        public ChangePasswordWithHistoryStrategy(
            IChangePasswordData changePasswordData,
            IPasswordHistoryBusiness passwordHistoryBusiness,
            IPasswordHasher passwordHasher)
        {
            this.changePasswordData = changePasswordData;
            this.passwordHistoryBusiness = passwordHistoryBusiness;
            this.passwordHasher = passwordHasher;
        }

        public async Task<ResponseResult> UpdatePassword(User user, string password)
        {
            // get history
            var items = await passwordHistoryBusiness.GetHistory(user.Id);
            if (items.Any())
            {
                // loop through history and check every hash against the new password
                foreach (var item in items)
                {
                    var checkResult = passwordHasher.Check(item.PasswordHash, password);
                    if (checkResult.Verified) // same password is being set.
                    {
                        return new ResponseResult { Success = false, Messages = new[] { string.Format(ErrorMessage_PasswordCheckRule, passwordHistoryBusiness.PasswordChangeHistoryRule) } };
                    }
                }
            }

            /* TODO: Consider using transaction */
            var newPasswordHash = passwordHasher.Hash(password);
            var result = await changePasswordData.Update(user.Id, newPasswordHash);
            if (result.Success)
            {
                await passwordHistoryBusiness.Create(user);
            }
            /* -------------------------------- */

            return result;
        }
    }
}
