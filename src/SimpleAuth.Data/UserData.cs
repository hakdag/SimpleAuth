using SimpleAuth.Common;
using SimpleAuth.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class UserData : BaseData<User>, IUserData
    {
        private readonly IRepository repository;

        public UserData(IRepository repository) : base(repository) { }

        public async Task<IEnumerable<User>> GetAll() => await RunQuery("SELECT id, username, lastlogindate FROM public.\"user\"");

        public async Task<User> GetByUserName(string userName)
        {
            var users = await RunQuery("SELECT id, username FROM public.\"user\" where username = @userName", new { userName });
            return users.FirstOrDefault();
        }

        public async Task<ResponseResult> Create(string userName, string passwordHash)
        {
            var result = await Execute("INSERT INTO public.\"user\"(username,password) VALUES(@userName, @passwordHash)", new { userName, passwordHash });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { "Error occured when creating the user." } };
        }

        public async Task UserLoggedIn(User user, string token, DateTime lastLoginDate)
            =>
                await Execute("UPDATE public.\"user\" SET token = @token, lastlogindate = @lastLoginDate WHERE username = @userName", new { userName = user.UserName, token, lastLoginDate });
    }
}
