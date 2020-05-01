using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class ChangePasswordData : BaseData<User>, IChangePasswordData
    {
        public ChangePasswordData(IRepository repository) : base(repository) { }

        public async Task<ResponseResult> Update(User user, string passwordHash)
        {
            var UpdatedDate = DateTime.Now;
            var result = await Execute("UPDATE public.user SET password = @passwordHash, updateddate = @UpdatedDate WHERE id = @id", new { id = user.Id, passwordHash, UpdatedDate });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { "Error occured when updating the user." } };
        }
    }
}
