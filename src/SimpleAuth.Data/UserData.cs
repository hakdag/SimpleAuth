using Dapper;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;

namespace SimpleAuth.Data
{
    public class UserData : IUserData
    {
        private readonly IRepository<User> repository;

        public UserData(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<User> GetAll() => repository.GetAll();

        public User GetByUserName(string userName)
        {
            return repository.Connection.QueryFirst<User>("SELECT id, username, password FROM public.\"user\" where username = @userName", new { userName });
        }
    }
}
