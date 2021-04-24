using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBE.Models
{
    public class LocationHistory
    {
        public int LocationHistoryId { get; set; }
        public int LocationId { get; set; }

        [ForeignKey(nameof(Employee)), Column(Order = 0)]
        public string EmpId { get; set; }

        public virtual CapstoneBEUser Employee { get; set; }
        public virtual Location Location { get; set; }
    }
}