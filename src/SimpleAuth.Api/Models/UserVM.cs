using System;

namespace SimpleAuth.Api.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string[] Roles { get; set; }
    }
}
