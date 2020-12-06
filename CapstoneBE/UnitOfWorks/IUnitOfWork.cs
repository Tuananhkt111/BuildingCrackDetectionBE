using CapstoneBE.Repositories.LocationHistories;
using CapstoneBE.Repositories.Locations;
using CapstoneBE.Repositories.MaintenanceWorkers;
using CapstoneBE.Repositories.Users;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace CapstoneBE.UnitOfWorks
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        MaintenanceWorkerRepository MaintenanceWorkerRepository { get; }
        LocationRepository LocationRepository { get; }
        LocationHistoryRepository LocationHistoryRepository { get; }

        Task<int> Save();

        IDbContextTransaction GetTransaction();
    }
}