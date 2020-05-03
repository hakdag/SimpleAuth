using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class PasswordHistoryBusiness : IPasswordHistoryBusiness
    {
        private readonly IPasswordHistoryData data;
        private readonly int passwordChangeHistoryRule;

        public PasswordHistoryBusiness(IPasswordHistoryData data, int passwordChangeHistoryRule)
        {
            this.data = data;
            this.passwordChangeHistoryRule = passwordChangeHistoryRule;
        }

        public int PasswordChangeHistoryRule => passwordChangeHistoryRule;

        public async Task<ResponseResult> Create(User user)
        {
            return await data.Create(user.Id, user.Password); // user.Password is the old password.
        }

        public async Task<IEnumerable<PasswordHistory>> GetHistory(long userId)
        {
            return await data.GetHistory(userId, passwordChangeHistoryRule);
        }
    }
}
