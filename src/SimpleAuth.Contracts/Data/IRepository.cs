using System;
using System.Data;

namespace SimpleAuth.Contracts.Data
{
    public interface IRepository : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
