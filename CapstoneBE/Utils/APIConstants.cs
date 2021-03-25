namespace CapstoneBE.Utils
{
    public static class APIConstants
    {
        public const string ForgotPassPathWeb = "http://localhost:8080/users/";

        public const string ForgotPassPathMobile = "https://bcdsys.azurewebsites.net/api/v1/users/";

        public static class Roles
        {
            public const string AdminRole = "Administrator";
            public const string ManagerRole = "Manager";
            public const string StaffRole = "Staff";
        }

        public static class MaintenanceOrderStatus
        {
            public const string WaitingForConfirm = "WaitingForConfirm";
            public const string WaitingForMaintenance = "WaitingForRepair";
            public const string Completed = "Completed";
        }

        public static class CrackSeverity
        {
            public const string Low = "Low"; // Nhung vet nut chua can thiet phai sua, dua vao danh sach theo doi
            public const string Medium = "Medium"; // Nhung vet nut o muc do co the bo qua trong lan sua toi
            public const string High = "High"; //Nhung vet nut bat buoc phai sua trong lan sua toi
        }

        public static class CrackStatus
        {
            public const string DetectedFailed = "DetectedFailed";
            public const string Unconfirmed = "Unconfirmed";
            public const string UnscheduledForMaintenace = "UnrecordedRepair";
            public const string ScheduledForMaintenace = "RecordedRepair";
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