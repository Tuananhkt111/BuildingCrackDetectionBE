using CapstoneBE.Models.Custom.Cracks;
using System;
using System.Collections.Generic;

namespace CapstoneBE.Models.Custom.MaintenanceOrders
{
    public class MaintenanceOrderInfo
    {
        public int MaintenanceOrderId { get; set; }
        public int? MaintenanceWorkerId { get; set; }
        public string MaintenanceWorkerName { get; set; }
        public int LocationId { get; set; }
        public int LocationName { get; set; }
        public string AssessorId { get; set; }
        public string AssessorName { get; set; }
        public string Description { get; set; }
        public int AssessmentResult { get; set; }
        public string Status { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public ICollection<CrackSubInfo> Cracks { get; set; }
    }
}