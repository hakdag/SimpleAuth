using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Business
{
    public class RoleBusiness : IRoleBusiness
    {
        private readonly IRoleData data;

        public readonly string ErrorMessage_RoleDoesNotExist = "Role does not exist.";
        public readonly string ErrorMessage_RoleExists = "Specified role already exists.";

        public RoleBusiness(IRoleData data)
        {
            this.data = data;
        }

        public async Task<IEnumerable<Role>> GetAll() => await data.GetAll();

        public async Task<Role> Get(long id) => await data.GetById(id);

        public async Task<ResponseResult> Create(string Name)
        {
            // check is username exists
            var existingRole = await data.GetByRoleName(Name);
            if (existingRole != null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_RoleExists } };
            }

            var response = await data.Create(Name);
            return response;
        }

        public async Task<ResponseResult> Update(long roleId, string newRoleName)
        {
            // check if role  exists
            var existingRole = await data.GetById(roleId);
            if (existingRole == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_RoleDoesNotExist } };
            }

            // check if rolename exists
            existingRole = await data.GetByRoleName(newRoleName);
            if (existingRole != null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_RoleExists } };
            }

            var response = await data.Update(roleId, newRoleName);
            return response;
        }

        public async Task<ResponseResult> Delete(long id)
        {
            // check if role  exists
            var existingRole = await data.GetById(id);
            if (existingRole == null)
            {
                return new ResponseResult { Success = false, Messages = new[] { ErrorMessage_RoleDoesNotExist } };
            }

            var response = await data.Delete(id);
            return response;
        }
    }
}
