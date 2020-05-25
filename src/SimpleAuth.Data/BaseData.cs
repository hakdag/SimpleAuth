using Dapper;
using SimpleAuth.Contracts.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Data
{
    public abstract class BaseData<T>
    {
        public BaseData(IRepository repository) => Repository = repository;

        protected IRepository Repository { get; }

        protected async Task<IEnumerable<T>> RunQuery(string sql, object param = null) => await Repository.Connection.QueryAsync<T>(sql, param);

        protected async Task<T[]> RunQueryAsArray(string sql, object param = null) => (await RunQuery(sql, param)).ToArray();

        protected async Task<T> RunQueryFirst(string sql, object param = null) => await Repository.Connection.QueryFirstOrDefaultAsync<T>(sql, param);

        protected async Task<int> Execute(string sql, object param = null) => await Repository.Connection.ExecuteAsync(sql, param);
    }
}
