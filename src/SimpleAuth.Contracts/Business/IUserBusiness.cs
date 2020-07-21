using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IUserBusiness
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByUserName(string userName);
        Task<User> Get(long userId);
        Task<ResponseResult> Create(string userName, string password);
        Task UserLoggedIn(User user, string token);
        Task<ResponseResult> Delete(long id);
        Task<ResponseResult> Update(long id, string newUserName);
        Task<ResponseResult> UserLoggedOut(string token);
    }
}
