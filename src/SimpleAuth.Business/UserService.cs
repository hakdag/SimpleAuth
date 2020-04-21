using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class UserService : IUserService
    {
        private readonly IUserData data;

        public readonly string ErrorMessage_UserNameExists = "UserName is taken.";

        public UserService(IUserData data) => this.data = data;

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

            return new ResponseResult { Success = true };
        }
    }
}
