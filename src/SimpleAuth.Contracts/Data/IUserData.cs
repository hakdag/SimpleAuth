using SimpleAuth.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IUserData
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetByUserName(string userName);
    }
}
