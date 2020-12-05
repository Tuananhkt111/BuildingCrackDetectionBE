using CapstoneBE.Models.Custom.MaintenaceWorkers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.MaintenanceWorkers
{
    public interface IMaintenanceWorkerService
    {
        Task<bool> Create(MaintenanceWorkerBasicInfo maintenanceWorkerBasicInfo);

        Task<bool> Delete(int id);

        Task<int> Update(MaintenanceWorkerBasicInfo maintenanceWorkerBasicInfo, int id);

        Task<MaintenanceWorkerInfo> GetById(int id);

        List<MaintenanceWorkerInfo> GetMaintenanceWorkers();

        int GetMaintenanceWorkersCount();
    }
}