using SimpleAuth.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IUserData
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetByUserName(string userName);
        Task<ResponseResult> Create(string userName, string passwordHash);
        Task UserLoggedIn(User user, string token, DateTime lastLoginDate);
    }
}
