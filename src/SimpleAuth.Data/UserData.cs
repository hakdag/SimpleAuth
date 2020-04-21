using Dapper;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Data;
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
    }
}
