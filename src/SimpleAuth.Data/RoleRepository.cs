using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public class RoleRepository : IRepository<Role>, IDisposable
    {
        private readonly string connectionString;
        private readonly IDbConnection connection;

        IDbConnection IRepository<Role>.Connection => connection;

        public RoleRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("DBInfo:ConnectionString").Value;
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await connection.QueryAsync<Role>("SELECT id, name FROM public.\"role\"");
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, bool force)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
