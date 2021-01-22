using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.MaintenanceOrders
{
    public interface IMaintenanceOrderRepository
    {
        Task<MaintenanceOrder> GetQueue(string userId);

        Task RemoveQueue(string userId);
    }
}