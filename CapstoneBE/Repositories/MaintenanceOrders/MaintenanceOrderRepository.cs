using CapstoneBE.Data;
using CapstoneBE.Models;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Repositories.MaintenanceOrders
{
    public class MaintenanceOrderRepository : GenericRepository<MaintenanceOrder>, IMaintenanceOrderRepository
    {
        public MaintenanceOrderRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public async Task<MaintenanceOrder> GetQueue(string userId)
        {
            return await GetSingle(filter: mo => mo.AssessorId.Equals(userId)
                && mo.Status.Equals(MaintenanceOrderStatus.WaitingForConfirm),
                includeProperties: "Cracks");
        }

        public void Delete(MaintenanceOrder maintenanceOrder)
        {
            _dbSet.Remove(maintenanceOrder);
        }
    }
}