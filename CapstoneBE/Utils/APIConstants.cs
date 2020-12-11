namespace CapstoneBE.Utils
{
    public static class APIConstants
    {
        public static class Roles
        {
            public const string AdminRole = "Administrator";
            public const string ManagerRole = "Manager";
            public const string StaffRole = "Staff";
        }

        public static class MaintenanceOrderStatus
        {
            public const string WaitingForConfirm = "Waiting for confirm";
            public const string WaitingForMaintenance = "Waiting for maintenance";
            public const string Completed = "Completed";
        }

        public static class CrackSeverity
        {
            public const string Low = "Low";
            public const string Medium = "Medium";
            public const string High = "High";
        }

        public static class CrackStatus
        {
            public const string Unconfirmed = "Unconfirmed";
            public const string UnscheduledForMaintenace = "Unscheduled for maintenace";
            public const string ScheduledForMaintenace = "Scheduled for maintenace";
            public const string Fixed = "Fixed";
        }
    }
}