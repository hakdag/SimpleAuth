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

        public UserService(IUserData data) => this.data = data;

        public async Task<IEnumerable<User>> GetAll() => await data.GetAll();

        public async Task<User> GetByUserName(string userName) => await data.GetByUserName(userName);
    }
}
