using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Models.Custom.Cracks
{
    public class CrackCreate
    {
        public string Position { get; set; }
        public string Status { get { return CrackStatus.Unconfirmed; } }
    }
}