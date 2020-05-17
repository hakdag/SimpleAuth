using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.PasswordReset;
using SimpleAuth.Contracts.Business.Strategies;
using SimpleAuth.Contracts.Data.PasswordReset;
using System.Threading.Tasks;

namespace SimpleAuth.Business.PasswordReset
{
    public class PasswordResetBusiness : IPasswordResetBusiness
    {
        public readonly string ErrorMessage_UserDoesNotExist = "User does not exist.";
        private readonly IPasswordResetData data;
        private readonly IUserBusiness userBusiness;
        private readonly IChangePasswordStrategy changePasswordStrategy;
        private readonly IValidatePasswordResetKeyBusiness validatePasswordResetKeyBusiness;

        public PasswordResetBusiness(
            IPasswordResetData data,
            IUserBusiness userBusiness,
            IChangePasswordStrategy changePasswordStrategy,
            IValidatePasswordResetKeyBusiness validatePasswordResetKeyBusiness)
        {
            this.data = data;
            this.userBusiness = userBusiness;
            this.changePasswordStrategy = changePasswordStrategy;
            this.validatePasswordResetKeyBusiness = validatePasswordResetKeyBusiness;
        }

        public async Task<ResponseResult> Reset(string userName, string resetKey, string password)
        {
            // check if username exists
            var user = await userBusiness.GetByUserName(userName);
            if (user == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesNotExist } };
            }

            // validate reset key
            var validateResponse = await validatePasswordResetKeyBusiness.Validate(user, resetKey);
            if (!validateResponse.Success)
            {
                return validateResponse;
            }

            // update user's password
            var updatePasswordResponse = await changePasswordStrategy.UpdatePassword(user, password);
            if (!updatePasswordResponse.Success)
            {
                return updatePasswordResponse;
            }

            // delete password reset key(s)
            return await data.RemoveKey(user.Id);
        }
    }
}
