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

        public readonly string ErrorMessage_RoleExists = "Specified role already exists.";

        public RoleBusiness(IRoleData data)
        {
            this.data = data;
        }

        public async Task<IEnumerable<Role>> GetAll() => await data.GetAll();

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
    }
}
