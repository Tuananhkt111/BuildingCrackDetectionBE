using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBE.Models
{
    public class Flight : BaseEntity
    {
        public Flight()
        {
            Cracks = new HashSet<Crack>();
        }

        public int FlightId { get; set; }
        public int LocationId { get; set; }

        [ForeignKey(nameof(DataCollector)), Column(Order = 0)]
        public string DataCollectorId { get; set; }

        [ForeignKey(nameof(DeleteVideoUser)), Column(Order = 1)]
        public string DeleteVideoUserId { get; set; }
        public string Video { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime? DeleteVideoDate { get; set; }
        public string Description { get; set; }
        public virtual Location Location { get; set; }
        public virtual CapstoneBEUser DataCollector { get; set; }
        public virtual CapstoneBEUser DeleteVideoUser { get; set; }
        public virtual ICollection<Crack> Cracks { get; set; }
    }
}