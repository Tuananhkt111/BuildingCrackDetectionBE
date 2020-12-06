using System;

namespace CapstoneBE.Models.Custom.LocationHistories
{
    public class LocationHistoryCreate
    {
        public int LocationId { get; set; }
        public string EmpId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
    }
}