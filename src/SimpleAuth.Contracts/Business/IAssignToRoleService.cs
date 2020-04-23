using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IAssignToRoleService
    {
        Task<ResponseResult> Create(int userId, int roleId);
    }
}
