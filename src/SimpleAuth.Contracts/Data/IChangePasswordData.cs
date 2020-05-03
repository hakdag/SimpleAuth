using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IChangePasswordData
    {
        Task<ResponseResult> Update(long userId, string passwordHash);
    }
}
