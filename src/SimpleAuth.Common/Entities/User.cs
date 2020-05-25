using System;

namespace SimpleAuth.Common.Entities
{
    public class User : BaseModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public DateTime? LastLoginDate { get; set; }
        public Role[] Roles { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
