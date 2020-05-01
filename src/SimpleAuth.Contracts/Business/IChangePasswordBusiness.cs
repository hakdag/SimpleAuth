using SimpleAuth.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Business
{
    public interface IChangePasswordBusiness
    {
        Task<ResponseResult> ChangePassword(string userName, string oldPassword, string password);
    }
}
