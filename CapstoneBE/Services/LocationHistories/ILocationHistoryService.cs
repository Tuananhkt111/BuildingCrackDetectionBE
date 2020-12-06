using CapstoneBE.Models.Custom.LocationHistories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.LocationHistories
{
    public interface ILocationHistoryService
    {
        Task<bool> Create(LocationHistoryCreate locationHistoryCreate);

        Task<bool> Delete(int id);

        Task<bool> Delete(int locationId, string userId);

        Task<bool> Update(LocationHistoryUpdate locationHistoryUpdate, int id);

        Task<LocationHistoryInfo> GetById(int id);

        List<LocationHistoryInfo> GetLocationHistories(string userId);

        int GetLocationHistoriesCount(string userId);
    }
}