using CapstoneBE.Data;
using CapstoneBE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.LocationHistories
{
    public class LocationHistoryRepository : GenericRepository<LocationHistory>, ILocationHistoryRepository
    {
        public LocationHistoryRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public void Create(int[] locationIds, string userId)
        {
            foreach (int locationId in locationIds)
            {
                LocationHistory locationHistory = new LocationHistory
                {
                    EmpId = userId,
                    LocationId = locationId
                };
                Create(locationHistory);
            }
        }

        public void Delete(LocationHistory locationHistory)
        {
            _dbSet.Remove(locationHistory);
        }

        public async Task<LocationHistory> GetById(int locationId, string userId)
        {
            return await GetSingle(filter: lh => lh.LocationId == locationId
                && lh.EmpId == userId);
        }

        public async Task<Location> GetLocationOfStaffById(string staffId)
        {
            LocationHistory locationHistory = await GetSingle(filter: lh => lh.EmpId == staffId,
                includeProperties: "Location");
            return locationHistory.Location;
        }

        public void Update(int[] locationIds, string userId)
        {
            if(locationIds != null && locationIds.Length > 0)
            {
                List<LocationHistory> locationHistories = Get(filter: lh => lh.EmpId.Equals(userId)).ToList();
                foreach (LocationHistory locationHistory in locationHistories)
                {
                    Delete(locationHistory);
                }
                Create(locationIds, userId);
            }
        }

        public void DeleteRange(string userId)
        {

            List<LocationHistory> locationHistories = Get(filter: lh => lh.EmpId.Equals(userId)).ToList();
            foreach (LocationHistory locationHistory in locationHistories)
            {
                Delete(locationHistory);
            }
        }
    }
}