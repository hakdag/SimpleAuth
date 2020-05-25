using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class PermissionBusiness : IPermissionBusiness
    {
        private readonly IPermissionData data;

        public readonly string ErrorMessage_PermissionDoesNotExist = "Permission with provided Id does not exist.";
        public readonly string ErrorMessage_PermissionExists = "Another permission with same name already exists.";

        public PermissionBusiness(IPermissionData data) => this.data = data;

        public async Task<ResponseResult> Create(string Name)
        {
            // check if permission name exists
            var existingPermission = await data.GetByPermissionName(Name);
            if (existingPermission != null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_PermissionExists } };
            }

            var response = await data.Create(Name);
            return response;
        }

        public async Task<ResponseResult> Delete(long id)
        {
            // check if permission exists
            var existingPermission = await data.GetById(id);
            if (existingPermission == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_PermissionDoesNotExist } };
            }

            var response = await data.Delete(id);
            return response;
        }

        public async Task<Permission> Get(long id) => await data.GetById(id);

        public async Task<IEnumerable<Permission>> GetAll() => await data.GetAll();

        public async Task<ResponseResult> Update(long id, string newPermissionName)
        {
            // check if permission exists
            var existingPermission = await data.GetById(id);
            if (existingPermission == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_PermissionDoesNotExist } };
            }

            // check if permission name exists
            existingPermission = await data.GetByPermissionName(newPermissionName);
            if (existingPermission != null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_PermissionExists } };
            }

            var response = await data.Update(id, newPermissionName);
            return response;
        }
    }
}
