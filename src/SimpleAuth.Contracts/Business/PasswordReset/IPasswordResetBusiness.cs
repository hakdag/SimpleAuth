using SimpleAuth.Common;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business.PasswordReset
{
    public interface IPasswordResetBusiness
    {
        Task<ResponseResult> Reset(string userName, string resetKey, string password);
    }
}
