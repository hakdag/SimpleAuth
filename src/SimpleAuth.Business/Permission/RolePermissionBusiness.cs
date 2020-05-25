using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class RolePermissionBusiness : IRolePermissionBusiness
    {
        private readonly IRolePermissionData data;
        private readonly IRoleData roleData;
        private readonly IPermissionData permissionData;

        public readonly string ErrorMessage_RoleDoesntExist = "Role couldn't be found.";
        public readonly string ErrorMessage_PermissionDoesntExist = "Permission couldn't be found.";

        public RolePermissionBusiness(
            IRolePermissionData data,
            IRoleData roleData,
            IPermissionData permissionData)
        {
            this.data = data;
            this.roleData = roleData;
            this.permissionData = permissionData;
        }

        public async Task<ResponseResult> Create(long roleId, long permissionId)
        {
            var checkResult = await CheckParameters(roleId, permissionId);
            if (checkResult != null)
            {
                return checkResult;
            }

            var rolePermission = await data.Get(roleId, permissionId);
            if (rolePermission != null)
            {
                return new ResponseResult { Success = true };
            }

            return await data.Create(roleId, permissionId);
        }

        public async Task<ResponseResult> Delete(long roleId, long permissionId)
        {
            var checkResult = await CheckParameters(roleId, permissionId);
            if (checkResult != null)
            {
                return checkResult;
            }

            return await data.Delete(roleId, permissionId);
        }

        private async Task<ResponseResult> CheckParameters(long roleId, long permissionId)
        {
            // check Role exists
            var Role = await roleData.GetById(roleId);
            if (Role == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_RoleDoesntExist } };
            }

            // check permission exists
            var role = await permissionData.GetById(permissionId);
            if (role == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_PermissionDoesntExist } };
            }

            return null;
        }
    }
}
