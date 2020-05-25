using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class UserPermissionBusiness : IUserPermissionBusiness
    {
        private readonly IUserPermissionData data;
        private readonly IUserData userData;
        private readonly IPermissionData permissionData;

        public readonly string ErrorMessage_UserDoesntExist = "User couldn't be found.";
        public readonly string ErrorMessage_PermissionDoesntExist = "Permission couldn't be found.";

        public UserPermissionBusiness(
            IUserPermissionData data,
            IUserData userData,
            IPermissionData permissionData)
        {
            this.data = data;
            this.userData = userData;
            this.permissionData = permissionData;
        }

        public async Task<ResponseResult> Create(long userId, long permissionId)
        {
            var checkResult = await CheckParameters(userId, permissionId);
            if (checkResult != null)
            {
                return checkResult;
            }

            var userPermission = await data.Get(userId, permissionId);
            if (userPermission != null)
            {
                return new ResponseResult { Success = true };
            }

            return await data.Create(userId, permissionId);
        }

        public async Task<ResponseResult> Delete(long userId, long permissionId)
        {
            var checkResult = await CheckParameters(userId, permissionId);
            if (checkResult != null)
            {
                return checkResult;
            }

            return await data.Delete(userId, permissionId);
        }

        private async Task<ResponseResult> CheckParameters(long userId, long permissionId)
        {
            // check user exists
            var user = await userData.GetById(userId);
            if (user == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesntExist } };
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
