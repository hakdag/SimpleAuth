﻿using SimpleAuth.Common;
using SimpleAuth.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAuth.Contracts.Data
{
    public interface IUserData
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByUserName(string userName);
        Task<User> GetById(long userId);
        Task<ResponseResult> Create(string userName, string passwordHash);
        Task UserLoggedIn(User user, string token, DateTime lastLoginDate);
        Task<ResponseResult> UserLoggedOut(string token);
        Task<ResponseResult> Delete(long id);
        Task<ResponseResult> Update(long id, string newUserName);
    }
}
