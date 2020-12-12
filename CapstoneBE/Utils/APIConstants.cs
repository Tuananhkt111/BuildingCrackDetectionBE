﻿namespace CapstoneBE.Utils
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

        public static class MessageType
        {
            public const string AdminUpdateInfo = "AdminUpdateInfo";
            public const string AdminChangeRole = "AdminChangeRole";
            public const string SystemFinishedDetection = "SystemFinishedDetection";
            public const string ManagerRequestStaff = "ManagerRequestStaff";
            public const string StaffCreateOrder = "StaffCreateOrder";
            public const string StaffUpdateOrder = "StaffUpdateOrder";
            public const string StaffEvaluateOrder = "StaffEvaluateOrder";
        }
    }
}