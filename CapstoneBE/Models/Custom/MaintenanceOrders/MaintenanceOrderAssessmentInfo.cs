using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Models.Custom.MaintenanceOrders
{
    public class MaintenanceOrderAssessmentInfo
    {
        public string Description { get; set; }
        public int AssessmentResult { get; set; }
        public string Status { get { return MaintenanceOrderStatus.Completed; } }
    }
}