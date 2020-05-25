using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IRolePermissionBusiness
    {
        Task<ResponseResult> Create(long roleId, long permissionId);
        Task<ResponseResult> Delete(long roleId, long permissionId);
    }
}
