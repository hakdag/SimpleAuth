using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IRoleData
    {
        Task<IEnumerable<Role>> GetAll();
        Task<ResponseResult> Create(string Name);
        Task<ResponseResult> Update(int roleId, string newRoleName);
        Task<ResponseResult> Delete(int id);
        Task<Role> GetByRoleName(string Name);
        Task<Role> GetById(int roleId);
        Task<Role[]> GetUserRoles(User user);
        Task SetUsersRoles(User[] users);
    }
}
