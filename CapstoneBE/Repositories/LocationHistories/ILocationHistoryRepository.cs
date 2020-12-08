using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.LocationHistories
{
    public interface ILocationHistoryRepository
    {
        void Delete(LocationHistory locationHistory);

        Task<LocationHistory> GetById(int locationId, string userId);

        void Update(int[] locationIds, string userId);

        void Create(int[] locationIds, string userId);
    }
}