namespace CapstoneBE.Utils
{
    public static class APIConstants
    {
        public const string ForgotPassPathWeb = "http://localhost:8080/users/";

        public const string ForgotPassPathMobile = "https://bcdsys.azurewebsites.net/api/v1/users";

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
            public const string DetectedFailed = "DetectedFailed";
            public const string Unconfirmed = "Unconfirmed";
            public const string UnscheduledForMaintenace = "Unscheduled for maintenance";
            public const string ScheduledForMaintenace = "Scheduled for maintenance";
            public const string Fixed = "Fixed";
        }

        public static class MessageTypes
        {
            public const string AdminUpdateInfo = "AdminUpdateInfo";
            public const string SystemFinishedDetection = "SystemFinishedDetection";
            public const string StaffCreateOrder = "StaffCreateOrder";
            public const string StaffUpdateOrder = "StaffUpdateOrder";
            public const string StaffEvaluateOrder = "StaffEvaluateOrder";
        }
    }
}