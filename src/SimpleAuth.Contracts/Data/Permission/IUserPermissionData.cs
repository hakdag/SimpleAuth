using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IUserPermissionData
    {
        Task<UserPermission> Get(long userId, long permissionId);
        Task<ResponseResult> Create(long userId, long permissionId);
        Task<ResponseResult> Delete(long userId, long permissionId);
    }
}
