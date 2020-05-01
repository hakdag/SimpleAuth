using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IChangePasswordData
    {
        Task<ResponseResult> Update(User user, string passwordHash);
    }
}
