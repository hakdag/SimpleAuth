using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IUserPermissionBusiness
    {
        Task<ResponseResult> Create(long userId, long permissionId);
        Task<ResponseResult> Delete(long userId, long permissionId);
    }
}
