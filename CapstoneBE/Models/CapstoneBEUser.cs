using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBE.Models
{
    public class CapstoneBEUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        //Use for firebase (in need)
        public string FcmToken { get; set; }

        [MaxLength(30)]
        public string Role { get; set; }

        [Required]
        public bool IsDel { get; set; }

        [Required]
        public bool IsNewUser { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

        [InverseProperty(nameof(MaintenanceOrder.Assessor))]
        public virtual ICollection<MaintenanceOrder> MaintenanceOrders { get; set; }

        [InverseProperty(nameof(Crack.Reporter))]
        public virtual ICollection<Crack> Cracks { get; set; }

        [InverseProperty(nameof(LocationHistory.Employee))]
        public virtual ICollection<LocationHistory> LocationHistories { get; set; }

        [InverseProperty(nameof(PushNotification.Sender))]
        public virtual ICollection<PushNotification> SentNotifications { get; set; }

        [InverseProperty(nameof(PushNotification.Receiver))]
        public virtual ICollection<PushNotification> ReceivedNotifications { get; set; }
    }
}