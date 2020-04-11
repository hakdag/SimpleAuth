using SimpleAuth.Common;
using System.Collections.Generic;

namespace SimpleAuth.Contracts.Data
{
    public interface IUserData
    {
        IEnumerable<User> GetAll();

        User GetByUserName(string userName);
    }
}
