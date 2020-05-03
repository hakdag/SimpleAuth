using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface ILockAccountBusiness
    {
        Task<ResponseResult> LockAccount(int userId);
    }
}
