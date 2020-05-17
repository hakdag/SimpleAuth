using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Business.Strategies;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Business.Strategies
{
    public class ChangePasswordStrategy : IChangePasswordStrategy
    {
        private readonly IChangePasswordData changePasswordData;

        public ChangePasswordStrategy(IChangePasswordData changePasswordData) => this.changePasswordData = changePasswordData;

        public async Task<ResponseResult> UpdatePassword(User user, string passwordHash) => await changePasswordData.Update(user.Id, passwordHash);
    }
}
