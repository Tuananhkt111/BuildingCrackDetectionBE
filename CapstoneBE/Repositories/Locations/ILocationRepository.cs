using CapstoneBE.Models.Custom.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Locations
{
    public interface ILocationRepository
    {
        Task Delete(int id);

        Task UpdateBasicInfo(LocationBasicInfo locationBasicInfo, int id);
    }
}