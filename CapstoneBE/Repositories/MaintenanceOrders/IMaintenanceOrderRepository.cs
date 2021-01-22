using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.MaintenanceOrders
{
    public interface IMaintenanceOrderRepository
    {
        Task<MaintenanceOrder> GetQueue(string userId);

        void Delete(MaintenanceOrder maintenanceOrder);
    }
}