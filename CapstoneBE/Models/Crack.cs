using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBE.Models
{
    public class Crack : BaseEntity
    {
        public int CrackId { get; set; }
        public int? MaintenanceOrderId { get; set; }
        public int FlightId { get; set; }

        [ForeignKey(nameof(Censor)), Column(Order = 0)]
        public string CensorId { get; set; }

        [ForeignKey(nameof(UpdateUser)), Column(Order = 1)]
        public string UpdateUserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Position { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public float Accuracy { get; set; }

        [MaxLength(10)]
        public string Severity { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string ImageThumbnails { get; set; }

        public int AssessmentResult { get; set; }
        public string AssessmentDescription { get; set; }
        public virtual CapstoneBEUser Censor { get; set; }
        public virtual CapstoneBEUser UpdateUser { get; set; }
        public virtual MaintenanceOrder MaintenanceOrder { get; set; }
        public virtual Flight Flight { get; set; }
    }
}