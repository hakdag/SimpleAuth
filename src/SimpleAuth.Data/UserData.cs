using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class UserData : BaseData<User>, IUserData
    {
        private readonly IRoleData roleData;
        private readonly IPermissionData permissionData;
        public readonly string ErrorMessage_DeleteUserError = "Error occured when deleting the user.";

        public UserData(
            IRepository repository,
            IRoleData roleData,
            IPermissionData permissionData) : base(repository)
        {
            this.roleData = roleData;
            this.permissionData = permissionData;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = (await RunQuery("SELECT id, username, lastlogindate FROM public.user where isdeleted = false")).ToArray();

            // set roles
            await roleData.SetUsersRoles(users);
            return users;
        }

        public async Task<User> GetByUserName(string userName)
        {
            // get user
            var user = await RunQueryFirst("SELECT id, username, password FROM public.\"user\" where username = @userName and isdeleted = false", new { userName });
            if (user == null)
            {
                return null;
            }

            // get user roles
            user.Roles = await roleData.GetUserRoles(user);
            return user;
        }

        public async Task<ResponseResult> Create(string userName, string passwordHash)
        {
            var result = await Execute("INSERT INTO public.\"user\"(username,password) VALUES(@userName, @passwordHash)", new { userName, passwordHash });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { "Error occured when creating the user." } };
        }

        public async Task<ResponseResult> Update(long id, string newUserName)
        {
            var UpdatedDate = DateTime.Now;
            var result = await Execute("UPDATE public.user SET username = @newUserName, updateddate = @UpdatedDate WHERE id = @id", new { id, newUserName, UpdatedDate });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { "Error occured when updating the user." } };
        }

        public async Task UserLoggedIn(User user, string token, DateTime lastLoginDate)
            =>
                await Execute("UPDATE public.user SET token = @token, lastlogindate = @lastLoginDate WHERE username = @userName", new { userName = user.UserName, token, lastLoginDate });

        public async Task<User> GetById(long userId)
        {
            // get user
            var user = await RunQueryFirst("SELECT id, username FROM public.\"user\" where id = @userId and isdeleted = false", new { userId });
            if (user == null)
            {
                return null;
            }

            // get user roles
            user.Roles = await roleData.GetUserRoles(user);

            // get user permissions
            user.Permissions = await permissionData.GetUserPermissions(user);
            return user;
        }

        public async Task<ResponseResult> Delete(long id)
        {
            var result = await Execute("UPDATE public.user SET isdeleted = true WHERE id = @id", new { id });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_DeleteUserError } };
        }
    }
}
