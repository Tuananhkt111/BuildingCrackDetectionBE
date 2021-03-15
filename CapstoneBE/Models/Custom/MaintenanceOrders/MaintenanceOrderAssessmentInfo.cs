using CapstoneBE.Models.Custom.Cracks;
using System;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Models.Custom.MaintenanceOrders
{
    public class MaintenanceOrderAssessmentInfo
    {
        public string Description { get; set; }
        public int AssessmentResult { get; set; }
        public CrackAssessmentInfo[] CrackAssessments { get; set; }
        public string Status { get { return MaintenanceOrderStatus.Completed; } }
        public DateTime AssessmentDate { get; set; }
    }
}