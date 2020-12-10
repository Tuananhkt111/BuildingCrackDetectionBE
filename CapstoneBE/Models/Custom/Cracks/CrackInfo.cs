namespace CapstoneBE.Models.Custom.Cracks
{
    public class CrackInfo
    {
        public int CrackId { get; set; }
        public int? MaintenanceOrderId { get; set; }
        public int LocationId { get; set; }
        public string ReporterId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
    }
}