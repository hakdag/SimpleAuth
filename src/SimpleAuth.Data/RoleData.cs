using Dapper;
using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class RoleData : BaseData<Role>, IRoleData
    {
        public readonly string ErrorMessage_CreateRoleError = "Error occured when creating the role.";
        public readonly string ErrorMessage_UpdateRoleError = "Error occured when updating the role.";
        public readonly string ErrorMessage_DeleteRoleError = "Error occured when deleting the role.";

        public RoleData(IRepository repository) : base(repository) { }

        public async Task<IEnumerable<Role>> GetAll() => await RunQuery("SELECT id, name FROM public.role where isdeleted <> true");

        public async Task<Role> GetByRoleName(string name)
        {
            var roles = await RunQuery("SELECT id, name FROM public.\"role\" where name = @name", new { name });
            return roles.FirstOrDefault();
        }

        public async Task<ResponseResult> Create(string name)
        {
            var result = await Execute("INSERT INTO public.\"role\"(name) VALUES(@name)", new { name });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_CreateRoleError } };
        }

        public async Task<ResponseResult> Update(long roleId, string newRoleName)
        {
            var UpdatedDate = DateTime.Now;
            var result = await Execute("UPDATE public.role SET name = @newRoleName, updateddate = @UpdatedDate WHERE id = @roleId", new { roleId, newRoleName, UpdatedDate });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UpdateRoleError } };
        }

        public async Task<ResponseResult> Delete(long id)
        {
            var result = await Execute("UPDATE public.role SET isdeleted = true WHERE id = @id", new { id });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_DeleteRoleError } };
        }

        public async Task<Role> GetById(long roleId)
        {
            var roles = await RunQuery("SELECT id, name FROM public.role where id = @roleId and isdeleted <> true", new { roleId });
            return roles.FirstOrDefault();
        }

        public async Task<Role[]> GetUserRoles(User user)
        {
            var roles = await RunQuery("select r.id, r.name from public.role r inner join public.userrole ur on r.id = ur.roleid where ur.userid = @userId and r.isdeleted <> true", new { userId = user.Id });
            return roles.ToArray();
        }

        public async Task SetUsersRoles(User[] users)
        {
            var ids = users.Select(u => u.Id).ToArray();
            var items = await Repository.Connection.QueryAsync<SetUserRole>("select ur.userid as \"UserId\", r.id as \"RoleId\", r.name as \"RoleName\" from public.role r inner join public.userrole ur on r.id = ur.roleid where ur.userid = ANY(@ids) and r.isdeleted <> true", new { ids });
            foreach (var user in users)
            {
                user.Roles = items.Where(r => r.UserId == user.Id).Select(r => new Role { Id = r.RoleId, Name = r.RoleName }).ToArray();
            }
        }

        private class SetUserRole
        {
            public int UserId { get; set; }
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }
    }
}
