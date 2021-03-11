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

        Task<MaintenanceOrderInfo> GetById(int id);

        int GetMaintenanceOrdersCount();
    }
}