using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.PasswordReset;
using SimpleAuth.Contracts.Data.PasswordReset;
using System.Threading.Tasks;

namespace SimpleAuth.Business.PasswordReset
{
    public class ValidatePasswordResetKeyBusiness : IValidatePasswordResetKeyBusiness
    {
        private readonly IPasswordResetData data;
        private readonly IUserBusiness userBusiness;

        public readonly string ErrorMessage_UserDoesNotExist = "User does not exist.";
        public readonly string ErrorMessage_PasswordResetKeyDoesNotExist = "Password Reset Key does not exist.";

        public ValidatePasswordResetKeyBusiness(IPasswordResetData data, IUserBusiness userBusiness)
        {
            this.data = data;
            this.userBusiness = userBusiness;
        }

        public async Task<ResponseResult> Validate(string userName, string resetKey)
        {
            // check if username exists
            var user = await userBusiness.GetByUserName(userName);
            if (user == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesNotExist } };
            }

            var passwordResetKey = await data.Get(user.Id, resetKey);
            if (passwordResetKey == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_PasswordResetKeyDoesNotExist } };
            }

            return new ResponseResult { Success = true };
        }
    }
}
