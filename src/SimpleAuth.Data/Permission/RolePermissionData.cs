using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class RolePermissionData : BaseData<RolePermission>, IRolePermissionData
    {
        public readonly string ErrorMessage_AssingError = "Error occured when assinging permission to the role.";
        public readonly string ErrorMessage_UnAssingError = "Error occured when unassinging permission from the role.";

        public RolePermissionData(IRepository repository) : base(repository) { }

        public async Task<ResponseResult> Create(long roleId, long permissionId)
        {
            var result = await Execute("INSERT INTO public.Rolepermission(roleid,permissionid) VALUES(@roleId, @permissionId)", new { roleId, permissionId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_AssingError } };
        }

        public async Task<ResponseResult> Delete(long roleId, long permissionId)
        {
            var result = await Execute("DELETE FROM public.rolepermission where roleid = @RoleId and permissionid = @permissionId", new { roleId, permissionId });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UnAssingError } };
        }

        public async Task<RolePermission> Get(long roleId, long permissionId)
            =>
                await RunQueryFirst("SELECT id, roleid, permissionid FROM public.rolepermission where roleid = @RoleId and permissionid = @permissionId", new { roleId, permissionId });
    }
}
