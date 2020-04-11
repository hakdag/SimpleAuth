using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleAuth.Data
{
    public class UserRepository : IRepository<User>, IDisposable
    {
        private readonly string connectionString;
        private readonly IDbConnection connection;

        IDbConnection IRepository<User>.Connection => connection;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("DBInfo:ConnectionString").Value;
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
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

        public IEnumerable<User> GetAll()
        {
            return connection.Query<User>("SELECT id, username, password FROM public.\"user\"");
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
