using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class ChangePasswordBusiness : IChangePasswordBusiness
    {
        private readonly IChangePasswordData data;
        private readonly IUserBusiness userBusiness;
        private readonly IPasswordHasher passwordHasher;

        public readonly string ErrorMessage_UserDoesNotExist = "User does not exist.";
        public readonly string ErrorMessage_WrongPassword = "Password is wrong.";
        public readonly string ErrorMessage_NewPasswordCannotBeSame = "New password cannot be same as the old one.";

        public ChangePasswordBusiness(IChangePasswordData data, IUserBusiness userBusiness, IPasswordHasher passwordHasher)
        {
            this.data = data;
            this.userBusiness = userBusiness;
            this.passwordHasher = passwordHasher;
        }

        public async Task<ResponseResult> ChangePassword(string userName, string oldPassword, string password)
        {
            // check is username exists
            var existingUser = await userBusiness.GetByUserName(userName);
            if (existingUser == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesNotExist } };
            }

            var oldPasswordHash = passwordHasher.Hash(oldPassword);
            var newPasswordHash = passwordHasher.Hash(password);

            // check old password correct or not
            if (oldPasswordHash.Equals(existingUser.Password))
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_WrongPassword } };
            }

            // old and new passwords can not be same
            if (newPasswordHash.Equals(existingUser.Password))
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_NewPasswordCannotBeSame } };
            }

            // TODO: new password should not be used before.

            return await data.Update(existingUser, newPasswordHash);
        }
    }
}
