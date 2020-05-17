using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business.PasswordReset
{
    public interface IValidatePasswordResetKeyBusiness
    {
        Task<ResponseResult> Validate(string userName, string resetKey);
    }
}
