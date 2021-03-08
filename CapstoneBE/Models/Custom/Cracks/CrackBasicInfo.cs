using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Models.Custom.Cracks
{
    public class CrackBasicInfo
    {
        public string Position { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public string Status { get { return CrackStatus.UnscheduledForMaintenace; } }
    }
}