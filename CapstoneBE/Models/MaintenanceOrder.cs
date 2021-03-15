using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBE.Models
{
    public class MaintenanceOrder : BaseEntity
    {
        public MaintenanceOrder()
        {
            Cracks = new HashSet<Crack>();
        }

        public int MaintenanceOrderId { get; set; }
        public int? MaintenanceWorkerId { get; set; }
        public int LocationId { get; set; }

        [ForeignKey(nameof(Assessor)), Column(Order = 0)]
        public string AssessorId { get; set; }

        [ForeignKey(nameof(CreateUser)), Column(Order = 1)]
        public string CreateUserId { get; set; }

        [ForeignKey(nameof(UpdateUser)), Column(Order = 2)]
        public string UpdateUserId { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Range(minimum: 0, maximum: 100)]
        public int AssessmentResult { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

        public DateTime MaintenanceDate { get; set; }
        public DateTime AssessmentDate { get; set; }

        public virtual MaintenanceWorker MaintenanceWorker { get; set; }
        public virtual Location Location { get; set; }
        public virtual CapstoneBEUser Assessor { get; set; }
        public virtual CapstoneBEUser CreateUser { get; set; }
        public virtual CapstoneBEUser UpdateUser { get; set; }
        public virtual ICollection<Crack> Cracks { get; set; }
    }
}