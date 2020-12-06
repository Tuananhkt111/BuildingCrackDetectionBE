using CapstoneBE.Data;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.LocationHistories;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.LocationHistories
{
    public class LocationHistoryRepository : GenericRepository<LocationHistory>, ILocationHistoryRepository
    {
        public LocationHistoryRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public async Task Delete(int locationId, string userId)
        {
            LocationHistory locationHistory = await GetById(locationId, userId);
            _dbSet.Remove(locationHistory);
        }

        public async Task Delete(int locationHistoryId)
        {
            LocationHistory locationHistory = await GetById(locationHistoryId);
            _dbSet.Remove(locationHistory);
        }

        public async Task<LocationHistory> GetById(int locationId, string userId)
        {
            return await GetSingle(filter: lh => lh.LocationId == locationId && lh.EmpId == userId);
        }

        public async Task Update(LocationHistoryUpdate locationHistoryUpdate, int locationHistoryId)
        {
            LocationHistory locationHistory = await GetById(locationHistoryId);
            if (locationHistory != null)
            {
                locationHistory.StartDate = locationHistoryUpdate.StartDate;
                locationHistory.EndDate = locationHistoryUpdate.EndDate;
            }
        }
    }
}