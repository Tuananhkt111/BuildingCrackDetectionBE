using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Locations;
using CapstoneBE.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Locations
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(LocationBasicInfo locationBasicInfo)
        {
            Location location = _mapper.Map<Location>(locationBasicInfo);
            _unitOfWork.LocationRepository.Create(location);
            return await _unitOfWork.Save() != 0;
        }

        public async Task<bool> Delete(int id)
        {
            _unitOfWork.LocationRepository.Delete(id).Wait();
            return await _unitOfWork.Save() != 0;
        }

        public async Task<LocationInfo> GetById(int id)
        {
            Location location = await _unitOfWork.LocationRepository.GetById(id);
            return _mapper.Map<LocationInfo>(location);
        }

        public List<LocationInfo> GetLocations()
        {
            return _unitOfWork.LocationRepository.Get(filter: l => !l.IsDel).Select(l => _mapper.Map<LocationInfo>(l)).ToList();
        }

        public int GetLocationsCount()
        {
            return _unitOfWork.LocationRepository.Get(filter: l => !l.IsDel).Select(l => _mapper.Map<LocationInfo>(l)).Count();
        }

        public async Task<int> Update(LocationBasicInfo locationBasicInfo, int id)
        {
            await _unitOfWork.LocationRepository.UpdateBasicInfo(locationBasicInfo, id);
            return await _unitOfWork.Save();
        }
    }
}