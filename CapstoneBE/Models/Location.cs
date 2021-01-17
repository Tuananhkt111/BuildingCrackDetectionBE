using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneBE.Models
{
    public class Location : BaseEntity
    {
        public Location()
        {
            Cracks = new HashSet<Crack>();
            LocationHistories = new HashSet<LocationHistory>();
        }

        public int LocationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Crack> Cracks { get; set; }
        public virtual ICollection<LocationHistory> LocationHistories { get; set; }
        public virtual MaintenanceOrder MaintenanceOrder { get; set; }

        [Required]
        public bool IsDel { get; set; }
    }
}