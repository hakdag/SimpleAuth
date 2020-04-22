using Dapper;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class RoleData : IRoleData
    {
        private readonly IRepository<Role> repository;

        public RoleData(IRepository<Role> repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<Role>> GetAll() => await repository.GetAll();

        public async Task<Role> GetByRoleName(string name)
        {
            var roles = await repository.Connection.QueryAsync<Role>("SELECT id, name FROM public.\"role\" where name = @name", new { name });
            return roles.FirstOrDefault();
        }

        public async Task<ResponseResult> Create(string name)
        {
            var result = await repository.Connection.ExecuteAsync(new CommandDefinition("INSERT INTO public.\"role\"(name) VALUES(@name)", new { name }));
            if (result > 0)
            {
                return new ResponseResult { Success = true };
            }

            return new ResponseResult { Success = false, Messages = new[] { "Error occured when creating the role." } };
        }
    }
}
