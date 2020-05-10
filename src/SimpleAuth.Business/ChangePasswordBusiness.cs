﻿using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.Strategies;
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
        private readonly IChangePasswordStrategy changePasswordStrategy;
        private readonly IUserBusiness userBusiness;
        private readonly IPasswordHasher passwordHasher;

        public readonly string ErrorMessage_UserDoesNotExist = "User does not exist.";
        public readonly string ErrorMessage_WrongPassword = "Password is wrong.";
        public readonly string ErrorMessage_NewPasswordCannotBeSame = "New password cannot be same as the old one.";

        public ChangePasswordBusiness(
            IChangePasswordData data,
            IChangePasswordStrategy changePasswordStrategy,
            IUserBusiness userBusiness,
            IPasswordHasher passwordHasher)
        {
            this.data = data;
            this.changePasswordStrategy = changePasswordStrategy;
            this.userBusiness = userBusiness;
            this.passwordHasher = passwordHasher;
        }

        public async Task<ResponseResult> ChangePassword(string userName, string oldPassword, string password)
        {
            // check if username exists
            var user = await userBusiness.GetByUserName(userName);
            if (user == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesNotExist } };
            }

            // check old password correct or not
            var checkResult = passwordHasher.Check(user.Password, oldPassword);
            if (!checkResult.Verified)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_WrongPassword } };
            }

            return await changePasswordStrategy.UpdatePassword(user, password);
        }
    }
}
