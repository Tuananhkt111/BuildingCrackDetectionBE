using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.Models.Custom.MaintenanceOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.MaintenanceOrders
{
    public interface IMaintenanceOrderService
    {
        Task<bool> AddToQueue(int[] crackIds);

        Task<bool> RemoveFromQueue(int[] crackIds);

        Task<List<CrackSubDetailsInfo>> GetQueue();

        Task<int> ConfirmOrder(MaintenanceOrderBasicInfo maintenanceOrderBasicInfo);

        Task<int> UpdateOrder(MaintenanceOrderBasicInfo maintenanceOrderBasicInfo, int orderId);

        Task<int> EvaluateOrder(MaintenanceOrderAssessmentInfo maintenanceOrderAssessmentInfo, int orderId);

        List<MaintenanceOrderInfo> GetMaintenanceOrders();

        List<MaintenanceOrderInfo> GetMaintenanceOrders(string status);

        Task<MaintenanceOrderInfo> GetById(int id);

        int GetMaintenanceOrdersCount();
        int GetMaintenanceOrdersCount(int year, int locationId);
        double GetMaintenanceOrdersAssessmentAverage(int year, int locationId);
        List<ChartValue> GetMaintenanceOrdersCountByStatus(int period, int year, int[] locationIds);
        List<ChartValueFloat> GetMaintenanceOrdersExpense(int year, int locationId);
        float GetMaintenanceOrdersExpenseTotal(int year, int locationId);
    }
}