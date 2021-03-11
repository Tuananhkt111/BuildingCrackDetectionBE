using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneBE.Models
{
    public class Location : BaseEntity
    {
        public Location()
        {
            Flights = new HashSet<Flight>();
            LocationHistories = new HashSet<LocationHistory>();
            MaintenanceOrders = new HashSet<MaintenanceOrder>();
        }

        public int LocationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
        public virtual ICollection<LocationHistory> LocationHistories { get; set; }
        public virtual ICollection<MaintenanceOrder> MaintenanceOrders { get; set; }

        [Required]
        public bool IsDel { get; set; }
    }
}