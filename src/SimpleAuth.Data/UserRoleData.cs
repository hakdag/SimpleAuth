using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class UserRoleData : BaseData<UserRole>, IUserRoleData
    {
        public readonly string ErrorMessage_AssingError = "Error occured when assinging role to the user.";
        public readonly string ErrorMessage_UnAssingError = "Error occured when unassinging role from the user.";

        public UserRoleData(IRepository repository) : base(repository) { }

        public async Task<ResponseResult> Create(int userId, int roleId)
        {
            var result = await Execute("INSERT INTO public.userrole(userid,roleid) VALUES(@userId, @roleId)", new { userId, roleId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_AssingError } };
        }

        public async Task<ResponseResult> Delete(int userId, int roleId)
        {
            var result = await Execute("DELETE FROM public.userrole where userid = @userId and roleid = @roleId", new { userId, roleId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UnAssingError } };
        }

        public async Task<UserRole> Get(int userId, int roleId)
            =>
                await RunQueryFirst("SELECT id, userid, roleid FROM public.userrole where userid = @userId and roleid = @roleId", new { userId, roleId });
    }
}
