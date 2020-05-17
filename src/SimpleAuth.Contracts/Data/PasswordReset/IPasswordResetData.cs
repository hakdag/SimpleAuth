using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data.PasswordReset
{
    public interface IPasswordResetData
    {
        Task<PasswordResetKeyResponse> StoreKey(long userId, string resetKey);
        Task<PasswordResetKey> Get(long userId, string resetKey);
    }
}
