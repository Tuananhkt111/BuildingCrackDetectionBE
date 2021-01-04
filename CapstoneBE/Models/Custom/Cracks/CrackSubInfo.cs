using System;

namespace CapstoneBE.Models.Custom.Cracks
{
    public class CrackSubInfo
    {
        public int CrackId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public int AssessmentResult { get; set; }
        public string AssessmentDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}