using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Flights;
using CapstoneBE.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;
using CapstoneBE.Utils;
using Azure.Storage.Blobs;

namespace CapstoneBE.Services.Flights
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetClaimsProvider _userData;
        private readonly IMapper _mapper;

        public FlightService(IUnitOfWork unitOfWork, IGetClaimsProvider userData, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userData = userData;
            _mapper = mapper;
        }

        public async Task<FlightBasicInfo> Create(FlightCreate flightCreate)
        {
            Flight flight = new()
            {
                Video = flightCreate.Video,
                RecordDate = flightCreate.RecordDate,
                Description = flightCreate.Description,
                DataCollectorId = _userData.UserId,
                LocationId = _userData.LocationIds.FirstOrDefault()
            };
            _unitOfWork.FlightRepository.Create(flight);
            bool result = await _unitOfWork.Save() != 0;
            return result ? _mapper.Map<FlightBasicInfo>(flight) : null;
        }

        public async Task<FlightInfo> GetById(int id)
        {
            Flight flight = await _unitOfWork.FlightRepository.GetSingle(filter: f => f.FlightId.Equals(id) 
                && ((_userData.LocationIds.Contains(f.LocationId)
                && !_userData.Role.Equals(Roles.AdminRole))
                || _userData.Role.Equals(Roles.AdminRole)), includeProperties: "Cracks,Location,DataCollector,DeleteVideoUser");
            return _mapper.Map<FlightInfo>(flight);
        }

        public List<FlightInfo> GetFlights()
        {
            return _unitOfWork.FlightRepository.Get(filter: f => (_userData.LocationIds.Contains(f.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                || _userData.Role.Equals(Roles.AdminRole), includeProperties: "Cracks,Location,DataCollector,DeleteVideoUser")
                .OrderByDescending(f => f.RecordDate)
                .Select(f => _mapper.Map<FlightInfo>(f))
                .ToList();
        }

        public int GetFlightsCount()
        {
            return _unitOfWork.FlightRepository.Get(filter: f => (_userData.LocationIds.Contains(f.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                || _userData.Role.Equals(Roles.AdminRole)).Count();
        }

        public async Task<bool> RemoveVideo(int id)
        {
            List<string> unacceptableCrackStatus = new()
            {
                CrackStatus.ScheduledForMaintenace,
                CrackStatus.Unconfirmed,
                CrackStatus.UnscheduledForMaintenace
            };
            //Check unacceptable crack exist or not
            bool isRemovable = !(_unitOfWork.CrackRepository.Get(filter: c => unacceptableCrackStatus.Contains(c.Status)
                && c.FlightId.Equals(id)).Any());
            if (!isRemovable)
                return false;
            Flight flight = await _unitOfWork.FlightRepository.GetById(id);
            string video = flight.Video;
            _unitOfWork.FlightRepository.RemoveVideo(flight);
            flight.DeleteVideoUserId = _userData.UserId;
            bool result = await _unitOfWork.Save() != 0;
            if(result)
            {
                AzureStorageHelper helper = new();
                BlobClient blobClient = helper.GetBlobClient("videos", video + ".mp4");
                if (helper.DeleteBlob(blobClient))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckExistsInDatabase(string video)
        {
            return _unitOfWork.FlightRepository.Get(filter: f => f.Video.Equals(video)).Any();
        }

        public bool CheckExistsInStorage(string video)
        {
            Flight flight = _unitOfWork.FlightRepository.Get(filter: f => f.Video.Equals(video)).FirstOrDefault();
            if (flight == null)
                return false;
            AzureStorageHelper helper = new();
            BlobClient blobClient = helper.GetBlobClient("videos", flight.Video + ".mp4");
            return helper.CheckBlobExists(blobClient);
        }
    }
}