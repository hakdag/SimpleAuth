using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IAssignToRoleData
    {
        Task<ResponseResult> Create(int userId, int roleId);
    }
}
