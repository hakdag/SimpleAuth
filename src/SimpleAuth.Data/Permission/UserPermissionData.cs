using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class UserPermissionData : BaseData<UserPermission>, IUserPermissionData
    {
        public readonly string ErrorMessage_AssingError = "Error occured when assinging permission to the user.";
        public readonly string ErrorMessage_UnAssingError = "Error occured when unassinging permission from the user.";

        public UserPermissionData(IRepository repository) : base(repository) { }

        public async Task<ResponseResult> Create(long userId, long permissionId)
        {
            var result = await Execute("INSERT INTO public.userpermission(userid,permissionid) VALUES(@userId, @permissionId)", new { userId, permissionId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_AssingError } };
        }

        public async Task<ResponseResult> Delete(long userId, long permissionId)
        {
            var result = await Execute("DELETE FROM public.userpermission where userid = @userId and permissionid = @permissionId", new { userId, permissionId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UnAssingError } };
        }

        public async Task<UserPermission> Get(long userId, long permissionId)
            =>
                await RunQueryFirst("SELECT id, userid, permissionid FROM public.userpermission where userid = @userId and permissionid = @permissionId", new { userId, permissionId });
    }
}
