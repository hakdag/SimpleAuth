using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Models
{
    public class UserVM
    {
        public string UserName { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
