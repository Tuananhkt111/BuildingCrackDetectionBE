using CapstoneBE.Models;
using CapstoneBE.Models.Custom.LocationHistories;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.LocationHistories
{
    public interface ILocationHistoryRepository
    {
        Task Delete(int locationId, string userId);

        Task Delete(int locationHistoryId);

        Task Update(LocationHistoryUpdate locationHistoryUpdate, int locationHistoryId);

        Task<LocationHistory> GetById(int locationId, string userId);
    }
}