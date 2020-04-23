using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
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
        private readonly IRoleData roleData;

        public UserData(IRepository repository, IRoleData roleData) : base(repository)
        {
            this.roleData = roleData;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = (await RunQuery("SELECT id, username, lastlogindate FROM public.\"user\"")).ToArray();

            // set roles
            await roleData.SetUsersRoles(users);
            return users;
        }

        public async Task<User> GetByUserName(string userName)
        {
            // get user
            var users = await RunQuery("SELECT id, username, password FROM public.\"user\" where username = @userName", new { userName });
            var user = users.FirstOrDefault();

            // get user roles
            user.Roles = await roleData.GetUserRoles(user);
            return user;
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

        public async Task<User> GetById(int userId)
        {
            var users = await RunQuery("SELECT id, username FROM public.\"user\" where id = @userId", new { userId });
            return users.FirstOrDefault();
        }
    }
}
