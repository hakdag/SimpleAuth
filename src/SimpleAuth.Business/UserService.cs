using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;

namespace SimpleAuth.Business
{
    public class UserService : IUserService
    {
        private readonly IUserData data;

        public UserService(IUserData data)
        {
            this.data = data;
        }

        public IEnumerable<User> GetAll() => data.GetAll();

        public User GetByUserName(string userName)
        {
            return data.GetByUserName(userName);
        }
    }
}
