using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IPermissionData
    {
        Task<IEnumerable<Permission>> GetAll();
        Task<ResponseResult> Create(string Name);
        Task<ResponseResult> Update(long id, string newPermissionName);
        Task<ResponseResult> Delete(long id);
        Task<Permission> GetByPermissionName(string name);
        Task<Permission> GetById(long permissionId);
        Task<Permission[]> GetUserPermissions(User user);
        Task SetUsersPermissions(User[] users);
        Task SetRolePermissions(Role[] roles);
        Task<Permission[]> GetRolePermissions(long roleId);
    }
}
