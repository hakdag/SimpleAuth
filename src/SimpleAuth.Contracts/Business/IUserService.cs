using SimpleAuth.Common;
using System.Collections.Generic;

namespace SimpleAuth.Contracts.Business
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetByUserName(string userName);
    }
}
