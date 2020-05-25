using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IPermissionBusiness
    {
        Task<IEnumerable<Permission>> GetAll();
        Task<Permission> Get(long id);
        Task<ResponseResult> Create(string Name);
        Task<ResponseResult> Update(long id, string newPermissionName);
        Task<ResponseResult> Delete(long id);
    }
}
