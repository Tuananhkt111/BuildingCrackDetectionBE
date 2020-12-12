using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBE.Models
{
    public class PushNotification : BaseEntity
    {
        public int PushNotificationId { get; set; }

        [ForeignKey(nameof(Sender)), Column(Order = 0)]
        public string SenderId { get; set; }

        [ForeignKey(nameof(Receiver)), Column(Order = 1)]
        public string ReceiverId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Body { get; set; }

        [Required]
        public string MessageType { get; set; }

        public virtual CapstoneBEUser Sender { get; set; }
        public virtual CapstoneBEUser Receiver { get; set; }
    }
}