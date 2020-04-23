using Microsoft.Extensions.Configuration;
using Npgsql;
using SimpleAuth.Contracts.Data;
using System.Data;

namespace SimpleAuth.Data
{
    public class PGRepository : IRepository
    {
        public PGRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("DBInfo:ConnectionString").Value;
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public IDbConnection Connection { get; }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
