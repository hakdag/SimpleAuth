using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IPasswordHistoryData
    {
        Task<IEnumerable<PasswordHistory>> GetHistory(long userId, int passwordChangeHistoryRule);
        Task<ResponseResult> Create(long userId, string passwordHash);
    }
}
