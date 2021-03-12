using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Flights;
using CapstoneBE.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorAPI.Utils;
using static CapstoneBE.Utils.APIConstants;

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

        public async Task<bool> Create()
        {
            DateTime curTime = DateTime.UtcNow;
            string video = curTime.Day.ToString() + "-" + curTime.Month.ToString() + "-" + curTime.Year.ToString();
            Flight flight = new()
            {
                Video = video,
                DataCollectorId = _userData.UserId,
                LocationId = _userData.LocationIds.FirstOrDefault()
            };
            _unitOfWork.FlightRepository.Create(flight);
            return await _unitOfWork.Save() != 0;
        }

        public async Task<FlightInfo> GetById(int id)
        {
            Flight flight = await _unitOfWork.FlightRepository.GetSingle(filter: f => (_userData.LocationIds.Contains(f.LocationId)
                && f.FlightId.Equals(id)
                && _userData.Role.Equals(Roles.ManagerRole))
                || _userData.Role.Equals(Roles.AdminRole), includeProperties: "Cracks,Location,DataCollector");
            return _mapper.Map<FlightInfo>(flight);
        }

        public List<FlightInfo> GetFlights()
        {
            return _unitOfWork.FlightRepository.Get(filter: l => (_userData.LocationIds.Contains(l.LocationId) && _userData.Role.Equals(Roles.ManagerRole))
                || _userData.Role.Equals(Roles.AdminRole), includeProperties: "Cracks,Location,DataCollector")
                .OrderByDescending(l => l.Created)
                .Select(l => _mapper.Map<FlightInfo>(l))
                .ToList();
        }

        public int GetFlightsCount()
        {
            return _unitOfWork.FlightRepository.Get(filter: l => (_userData.LocationIds.Contains(l.LocationId) && _userData.Role.Equals(Roles.ManagerRole))
                || _userData.Role.Equals(Roles.AdminRole)).Count();
        }
    }
}