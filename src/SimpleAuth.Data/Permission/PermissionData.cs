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
    public class PermissionData : BaseData<Permission>, IPermissionData
    {
        public readonly string ErrorMessage_CreatePermissionError = "Error occured when creating the permission.";
        public readonly string ErrorMessage_UpdatePermissionError = "Error occured when updating the permission.";
        public readonly string ErrorMessage_DeletePermissionError = "Error occured when deleting the permission.";

        public PermissionData(IRepository repository) : base(repository) { }

        public async Task<ResponseResult> Create(string name)
        {
            var result = await Execute("INSERT INTO public.permission(name) VALUES(@name)", new { name });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_CreatePermissionError } };
        }

        public async Task<ResponseResult> Delete(long id)
        {
            var result = await Execute("UPDATE public.permission SET isdeleted = true WHERE id = @id", new { id });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_DeletePermissionError } };
        }

        public async Task<IEnumerable<Permission>> GetAll() => await RunQuery("SELECT id, name FROM public.permission where isdeleted <> true");

        public async Task<Permission> GetById(long permissionId)
            =>
                await RunQueryFirst("SELECT id, name FROM public.permission where id = @permissionId and isdeleted <> true", new { permissionId });

        public async Task<Permission> GetByPermissionName(string name)
            =>
                await RunQueryFirst("SELECT id, name FROM public.permission where name = @name", new { name });

        public async Task<Permission[]> GetRolePermissions(long roleId)
            =>
                await RunQueryAsArray("select p.id, p.name from public.permission p inner join public.rolepermission rp on p.id = rp.permissionid where rp.roleid = @roleId and p.isdeleted <> true", new { roleId });

        public async Task<Permission[]> GetUserPermissions(User user)
            =>
                await RunQueryAsArray("select p.id, p.name from public.permission p inner join public.userpermission up on p.id = up.permissionid where up.userid = @userId and p.isdeleted <> true", new { userId = user.Id });

        public async Task SetUsersPermissions(User[] users)
        {
            var ids = users.Select(u => u.Id).ToArray();
            var items = await Repository.Connection.QueryAsync<SetUserPermission>("select up.userid as \"UserId\", p.id as \"PermissionId\", p.name as \"PermissionName\" from public.permission p inner join public.userpermission up on p.id = up.permissionid where up.userid = ANY(@ids) and p.isdeleted <> true", new { ids });
            foreach (var user in users)
            {
                user.Roles = items.Where(r => r.UserId == user.Id).Select(r => new Role { Id = r.PermissionId, Name = r.PermissionName }).ToArray();
            }
        }

        public async Task SetRolePermissions(Role[] roles)
        {
            var ids = roles.Select(u => u.Id).ToArray();
            var items = await Repository.Connection.QueryAsync<SetRolePermission>("select rp.roleid as \"RoleId\", p.id as \"PermissionId\", p.name as \"PermissionName\" from public.permission p inner join public.rolepermission rp on p.id = rp.permissionid where rp.roleid = ANY(@ids) and p.isdeleted <> true", new { ids });
            foreach (var role in roles)
            {
                role.Permissions = items.Where(r => r.RoleId == role.Id).Select(r => new Permission { Id = r.PermissionId, Name = r.PermissionName }).ToArray();
            }
        }

        public async Task<ResponseResult> Update(long permissionId, string newPermissionName)
        {
            var UpdatedDate = DateTime.Now;
            var result = await Execute("UPDATE public.permission SET name = @newPermissionName, updateddate = @UpdatedDate WHERE id = @permissionId", new { permissionId, newPermissionName, UpdatedDate });
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_UpdatePermissionError } };
        }

        private class SetUserPermission
        {
            public int UserId { get; set; }
            public int PermissionId { get; set; }
            public string PermissionName { get; set; }
        }

        private class SetRolePermission
        {
            public int RoleId { get; set; }
            public int PermissionId { get; set; }
            public string PermissionName { get; set; }
        }
    }
}
