using System.ComponentModel.DataAnnotations;

namespace SimpleAuth.Common
{
    public class Role : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
