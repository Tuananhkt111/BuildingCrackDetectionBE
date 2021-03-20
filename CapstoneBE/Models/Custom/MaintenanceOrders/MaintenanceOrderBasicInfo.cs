using System;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Models.Custom.MaintenanceOrders
{
    public class MaintenanceOrderBasicInfo
    {
        public int? MaintenanceWorkerId { get; set; }
        public float MaintenanceExpense { get; set; }
        public string Status { get { return MaintenanceOrderStatus.WaitingForMaintenance; } }
        public DateTime MaintenanceDate { get; set; }
    }
}