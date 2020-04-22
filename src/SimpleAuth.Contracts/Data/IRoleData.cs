using SimpleAuth.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IRoleData
    {
        Task<IEnumerable<Role>> GetAll();
        Task<ResponseResult> Create(string Name);
        Task<Role> GetByRoleName(string Name);
    }
}
