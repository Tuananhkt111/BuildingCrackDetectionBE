using System;

namespace CapstoneBE.Models.Custom.LocationHistories
{
    public class LocationHistoryInfo
    {
        public int LocationHistoryId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}