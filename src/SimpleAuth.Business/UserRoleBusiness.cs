using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class UserRoleBusiness : IUserRoleBusiness
    {
        private readonly IUserRoleData data;
        private readonly IUserData userData;
        private readonly IRoleData roleData;

        public readonly string ErrorMessage_UserDoesntExist = "User couldn't be found.";
        public readonly string ErrorMessage_RoleDoesntExist = "Role couldn't be found.";

        public UserRoleBusiness(IUserRoleData data, IUserData userData, IRoleData roleData)
        {
            this.data = data;
            this.userData = userData;
            this.roleData = roleData;
        }

        public async Task<ResponseResult> Create(int userId, int roleId)
        {
            // check user exists
            var user = await userData.GetById(userId);
            if (user == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UserDoesntExist } };
            }

            // check role exists
            var role = await roleData.GetById(roleId);
            if (role == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_RoleDoesntExist } };
            }

            return await data.Create(userId, roleId);
        }
    }
}
