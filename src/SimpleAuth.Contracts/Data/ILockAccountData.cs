using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface ILockAccountData
    {
        Task<ResponseResult> LockAccount(long userId);
        Task<ResponseResult> UnLockAccount(long userId);
        Task<ResponseResult> CheckUser(long userId);
    }
}
