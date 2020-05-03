using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business.Strategies
{
    public interface IChangePasswordStrategy
    {
        Task<ResponseResult> UpdatePassword(User user, string password);
    }
}
