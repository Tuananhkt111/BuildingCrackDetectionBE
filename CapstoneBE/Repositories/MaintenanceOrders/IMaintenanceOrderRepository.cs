using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.MaintenanceOrders
{
    public interface IMaintenanceOrderRepository
    {
        Task Delete(int id);

        void CreateRange(Crack[] cracks);

        void DeleteRange(int[] ids);
    }
}