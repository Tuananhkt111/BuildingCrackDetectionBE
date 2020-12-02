using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneBE.Models
{
    public class MaintenanceWorker : BaseEntity
    {
        public MaintenanceWorker()
        {
            MaintenanceOrders = new HashSet<MaintenanceOrder>();
        }

        public int MaintenanceWorkerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public virtual ICollection<MaintenanceOrder> MaintenanceOrders { get; set; }

        [Required]
        public bool IsDel { get; set; }
    }
}