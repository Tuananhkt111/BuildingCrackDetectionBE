using CapstoneBE.Repositories.Cracks;
using CapstoneBE.Repositories.LocationHistories;
using CapstoneBE.Repositories.Locations;
using CapstoneBE.Repositories.MaintenanceOrders;
using CapstoneBE.Repositories.MaintenanceWorkers;
using CapstoneBE.Repositories.PushNotifications;
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
        CrackRepository CrackRepository { get; }
        MaintenanceOrderRepository MaintenanceOrderRepository { get; }
        NotificationRepository NotificationRepository { get; }

        Task<int> Save();

        IDbContextTransaction GetTransaction();
    }
}