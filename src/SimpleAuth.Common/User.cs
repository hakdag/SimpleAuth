using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleAuth.Common
{
    public class User : BaseModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Token { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}
