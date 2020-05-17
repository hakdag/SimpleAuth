using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data.PasswordReset
{
    public interface IGeneratePasswordResetKeyData
    {
        Task<PasswordResetKeyResponse> StoreKey(long userId, string resetKey);
    }
}
