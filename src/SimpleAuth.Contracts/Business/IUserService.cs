using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByUserName(string userName);
        Task<ResponseResult> Create(string userName, string password);
        Task UserLoggedIn(User user, string token);
    }
}
