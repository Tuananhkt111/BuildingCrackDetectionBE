using System;

namespace CapstoneBE.Models.Custom.Cracks
{
    public class CrackInfo
    {
        public int CrackId { get; set; }
        public int? MaintenanceOrderId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string ReporterId { get; set; }
        public string ReporterName { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public float Accuracy { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public int AssessmentResult { get; set; }
        public string AssessmentDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}