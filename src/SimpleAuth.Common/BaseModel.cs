using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleAuth.Common
{
    public abstract class BaseModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
