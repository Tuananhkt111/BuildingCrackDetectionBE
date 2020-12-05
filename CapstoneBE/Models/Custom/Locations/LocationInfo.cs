using System;

namespace CapstoneBE.Models.Custom.Locations
{
    public class LocationInfo
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}