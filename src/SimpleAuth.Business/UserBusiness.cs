using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserData data;

        public readonly string ErrorMessage_UserNameExists = "UserName is taken.";

        public UserBusiness(IPasswordHasher passwordHasher, IUserData data)
        {
            this.passwordHasher = passwordHasher;
            this.data = data;
        }

        public async Task<IEnumerable<User>> GetAll() => await data.GetAll();

        public async Task<User> GetByUserName(string userName) => await data.GetByUserName(userName);

        public async Task<ResponseResult> Create(string userName, string password)
        {
            // check is username exists
            var existingUser = await data.GetByUserName(userName);
            if (existingUser != null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserNameExists } };
            }

            var passwordHash = passwordHasher.Hash(password);
            var response = await data.Create(userName, passwordHash);
            return response;
        }

        public async Task UserLoggedIn(User user, string token)
        {
            await data.UserLoggedIn(user, token, DateTime.Now);
        }
    }
}
