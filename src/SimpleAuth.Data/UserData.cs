using Dapper;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class UserData : IUserData
    {
        private readonly IRepository<User> repository;

        public UserData(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<User>> GetAll() => await repository.GetAll();

        public async Task<User> GetByUserName(string userName)
        {
            var users = await repository.Connection.QueryAsync<User>("SELECT id, username, password FROM public.\"user\" where username = @userName", new { userName });
            return users.FirstOrDefault();
        }

        public async Task<ResponseResult> Create(string userName, string passwordHash)
        {
            var result = await repository.Connection.ExecuteAsync(new CommandDefinition("INSERT INTO public.\"user\"(username,password) VALUES(@userName, @passwordHash)", new { userName, passwordHash }));
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { "Error occured when creating the user." } };
        }

        public async Task UserLoggedIn(User user, string token, DateTime lastLoginDate)
        {
            await repository.Connection.ExecuteAsync(new CommandDefinition("UPDATE public.\"user\" SET token = @token, lastlogindate = @lastLoginDate WHERE username = @userName", new { userName = user.UserName, token, lastLoginDate }));
        }
    }
}
