using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Common.Entities
{
    public class UserAccountLock : BaseModel
    {
        public int UserId { get; set; }
    }
}
