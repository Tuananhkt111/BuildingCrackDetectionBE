using CapstoneBE.Data;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Locations
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public async Task Delete(int id)
        {
            Location location = await GetById(id);
            if (location != null)
                location.IsDel = true;
        }

        public async Task UpdateBasicInfo(LocationBasicInfo locationBasicInfo, int id)
        {
            Location location = await GetById(id);
            if (location != null)
            {
                if (!String.IsNullOrEmpty(locationBasicInfo.Name))
                    location.Name = locationBasicInfo.Name;
                if (!String.IsNullOrEmpty(locationBasicInfo.Description))
                    location.Description = locationBasicInfo.Description;
            }
        }
    }
}