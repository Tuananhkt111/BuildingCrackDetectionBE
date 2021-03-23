using CapstoneBE.Models.Custom.Cracks;
using System;
using System.Collections.Generic;

namespace CapstoneBE.Models.Custom.Flights
{
    public class FlightInfo
    {
        public int FlightId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string DataCollectorId { get; set; }
        public string DataCollectorName { get; set; }
        public string Video { get; set; }
        public DateTime RecordDate { get; set; }
        public ICollection<CrackSubDetailsInfo> Cracks { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}