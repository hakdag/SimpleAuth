using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IRolePermissionData
    {
        Task<RolePermission> Get(long roleId, long permissionId);
        Task<ResponseResult> Create(long roleId, long permissionId);
        Task<ResponseResult> Delete(long roleId, long permissionId);
    }
}
