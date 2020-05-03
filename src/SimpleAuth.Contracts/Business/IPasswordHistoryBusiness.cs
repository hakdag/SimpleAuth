using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IPasswordHistoryBusiness
    {
        int PasswordChangeHistoryRule { get; }

        Task<ResponseResult> Create(User user);
        Task<IEnumerable<PasswordHistory>> GetHistory(long userId);
    }
}
