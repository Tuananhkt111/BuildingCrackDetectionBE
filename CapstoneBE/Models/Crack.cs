﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBE.Models
{
    public class Crack : BaseEntity
    {
        public int CrackId { get; set; }
        public int? MaintenanceOrderId { get; set; }
        public int LocationId { get; set; }

        [ForeignKey(nameof(Reporter)), Column(Order = 0)]
        public string ReporterId { get; set; }

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
        public virtual CapstoneBEUser Reporter { get; set; }
        public virtual MaintenanceOrder MaintenanceOrder { get; set; }
        public virtual Location Location { get; set; }
    }
}