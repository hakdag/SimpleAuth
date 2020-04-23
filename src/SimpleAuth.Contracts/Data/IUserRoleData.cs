using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IUserRoleData
    {
        Task<ResponseResult> Create(int userId, int roleId);
        Task<ResponseResult> Delete(int userId, int roleId);
        Task<UserRole> Get(int userId, int roleId);
    }
}
