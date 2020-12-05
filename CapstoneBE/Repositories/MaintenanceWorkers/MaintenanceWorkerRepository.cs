using CapstoneBE.Data;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.MaintenaceWorkers;
using System;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.MaintenanceWorkers
{
    public class MaintenanceWorkerRepository : GenericRepository<MaintenanceWorker>, IMaintenanceWorkerRepository
    {
        public MaintenanceWorkerRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public async Task Delete(int id)
        {
            MaintenanceWorker worker = await GetById(id);
            if (worker != null)
                worker.IsDel = true;
        }

        public async Task UpdateBasicInfo(MaintenanceWorkerBasicInfo workerInfo, int id)
        {
            MaintenanceWorker worker = await GetById(id);
            if (worker != null)
            {
                if (!String.IsNullOrEmpty(workerInfo.Name))
                    worker.Name = workerInfo.Name;
                if (!String.IsNullOrEmpty(workerInfo.Address))
                    worker.Address = workerInfo.Address;
                if (!String.IsNullOrEmpty(workerInfo.Phone))
                    worker.Phone = workerInfo.Phone;
                if (!String.IsNullOrEmpty(workerInfo.Email))
                    worker.Email = workerInfo.Email;
            }
        }
    }
}