using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.LocationHistories;
using CapstoneBE.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Services.LocationHistories
{
    public class LocationHistoryService : ILocationHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(LocationHistoryCreate locationHistoryCreate)
        {
            LocationHistory locationHistory = _mapper.Map<LocationHistory>(locationHistoryCreate);
            _unitOfWork.LocationHistoryRepository.Create(locationHistory);
            return await _unitOfWork.Save() != 0;
        }

        public async Task<bool> Delete(int id)
        {
            await _unitOfWork.LocationHistoryRepository.Delete(id);
            return await _unitOfWork.Save() != 0;
        }

        public async Task<bool> Delete(int locationId, string userId)
        {
            await _unitOfWork.LocationHistoryRepository.Delete(locationId, userId);
            return await _unitOfWork.Save() != 0;
        }

        public async Task<LocationHistoryInfo> GetById(int id)
        {
            LocationHistory locationHistory = await _unitOfWork.LocationHistoryRepository
                .GetSingle(filter: lh => lh.LocationHistoryId.Equals(id), includeProperties: "Location");
            return _mapper.Map<LocationHistoryInfo>(locationHistory);
        }

        public List<LocationHistoryInfo> GetLocationHistories(string userId)
        {
            return _unitOfWork.LocationHistoryRepository
                .Get(filter: lh => lh.EmpId.Equals(userId), includeProperties: "Location")
                .Select(lh => _mapper.Map<LocationHistoryInfo>(lh)).ToList();
        }

        public int GetLocationHistoriesCount(string userId)
        {
            return _unitOfWork.LocationHistoryRepository.Get(filter: lh => lh.EmpId.Equals(userId)).Count();
        }

        public async Task<bool> Update(LocationHistoryUpdate locationHistoryUpdate, int id)
        {
            await _unitOfWork.LocationHistoryRepository.Update(locationHistoryUpdate, id);
            return await _unitOfWork.Save() != 0;
        }
    }
}