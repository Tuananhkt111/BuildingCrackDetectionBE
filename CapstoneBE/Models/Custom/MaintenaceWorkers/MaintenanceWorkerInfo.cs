using System;

namespace CapstoneBE.Models.Custom.MaintenaceWorkers
{
    public class MaintenanceWorkerInfo
    {
        public int MaintenanceWorkerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}