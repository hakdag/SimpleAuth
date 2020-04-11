using SimpleAuth.Common;
using System.Collections.Generic;
using System.Data;

namespace SimpleAuth.Contracts.Data
{
    public interface IRepository<T> where T : BaseModel
    {
        IDbConnection Connection { get; }
        IEnumerable<T> GetAll();
        T GetById(int id);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Delete(int id, bool force);
    }
}
