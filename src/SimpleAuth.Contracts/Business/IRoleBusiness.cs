using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IRoleBusiness
    {
        Task<IEnumerable<Role>> GetAll();
        Task<ResponseResult> Create(string Name);
    }
}
