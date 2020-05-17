using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business.PasswordReset
{
    public interface IValidatePasswordResetKeyBusiness
    {
        Task<ResponseResult> Validate(string userName, string resetKey);
        Task<ResponseResult> Validate(User user, string resetKey);
    }
}
