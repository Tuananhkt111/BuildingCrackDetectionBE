using System;
using System.ComponentModel.DataAnnotations;

namespace CapstoneBE.Models
{
    public class BaseEntity
    {
        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastModified { get; set; }
    }
}