using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business.PasswordReset
{
    public interface IGeneratePasswordResetKeyBusiness
    {
        Task<PasswordResetKeyResponse> Generate(string userName);
    }
}
