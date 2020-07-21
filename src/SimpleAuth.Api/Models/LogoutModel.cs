using System.ComponentModel.DataAnnotations;

namespace SimpleAuth.Api.Models
{
    public class LogoutModel
    {
        [Required]
        public string Token { get; set; }
    }
}
