using CapstoneBE.Data;
using CapstoneBE.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.MaintenanceOrders
{
    public class MaintenanceOrderRepository : GenericRepository<MaintenanceOrder>
    {
        public MaintenanceOrderRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }
    }
}