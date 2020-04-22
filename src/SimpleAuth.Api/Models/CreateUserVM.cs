using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAuth.Api.Models
{
    public class CreateUserVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
