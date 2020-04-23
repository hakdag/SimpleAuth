using Dapper;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class RoleData : BaseData<Role>, IRoleData
    {
        public RoleData(IRepository repository) : base(repository) { }

        public async Task<IEnumerable<Role>> GetAll() => await RunQuery("SELECT id, name FROM public.\"role\"");

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

            return new ResponseResult { Success = false, Messages = new[] { "Error occured when creating the role." } };
        }
    }
}
