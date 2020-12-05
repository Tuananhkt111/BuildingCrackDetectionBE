using CapstoneBE.Models.Custom.MaintenaceWorkers;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.MaintenanceWorkers
{
    public interface IMaintenanceWorkerRepository
    {
        Task Delete(int id);

        Task UpdateBasicInfo(MaintenanceWorkerBasicInfo workerInfo, int id);
    }
}