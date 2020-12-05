using CapstoneBE.Models.Custom.Locations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Locations
{
    public interface ILocationService
    {
        Task<bool> Create(LocationBasicInfo locationBasicInfo);

        Task<bool> Delete(int id);

        Task<int> Update(LocationBasicInfo locationBasicInfo, int id);

        Task<LocationInfo> GetById(int id);

        List<LocationInfo> GetLocations();

        int GetLocationsCount();
    }
}