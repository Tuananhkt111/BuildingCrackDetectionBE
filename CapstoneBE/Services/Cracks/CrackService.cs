﻿using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneBE.Utils;
using static CapstoneBE.Utils.APIConstants;
using System;

namespace CapstoneBE.Services.Cracks
{
    public class CrackService : ICrackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetClaimsProvider _userData;
        private readonly IMapper _mapper;

        public CrackService(IUnitOfWork unitOfWork, IGetClaimsProvider userData, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userData = userData;
            _mapper = mapper;
        }

        public async Task<int> Create(CrackCreate crackCreate)
        {
            Crack crack = _mapper.Map<Crack>(crackCreate);
            crack.FlightId = crackCreate.FlightId;
            _unitOfWork.CrackRepository.Create(crack);
            bool result = await _unitOfWork.Save() != 0;
            return result ? crack.CrackId : -1;
        }

        public async Task<bool> CreateRange(CrackCreate[] crackCreates)
        {
            Crack[] cracks = crackCreates.Select(c => _mapper.Map<Crack>(c)).ToArray();
            foreach (Crack crack in cracks)
            {
                crack.FlightId = crack.FlightId;
            }
            _unitOfWork.CrackRepository.CreateRange(cracks);
            return await _unitOfWork.Save() == cracks.Length;
        }

        public async Task<bool> Delete(int id)
        {
            _unitOfWork.CrackRepository.Delete(id).Wait();
            return await _unitOfWork.Save() != 0;
        }

        public async Task<bool> DeleteRange(int[] ids)
        {
            _unitOfWork.CrackRepository.DeleteRange(ids);
            return await _unitOfWork.Save() == ids.Length;
        }

        public async Task<CrackInfo> GetById(int id)
        {
            return (await _unitOfWork.CrackRepository
                .Get(filter: c => !c.Status.Equals(CrackStatus.DetectedFailed) && c.CrackId.Equals(id), includeProperties: "Censor,UpdateUser")
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)))
                .OrderByDescending(c => c.Created)
                .ThenBy(c => c.Status)
                .ThenBy(c => c.Severity)
                .ThenBy(c => c.CrackId)
                .Select(c => _mapper.Map<CrackInfo>(c))
                .ToListAsync())
                .FirstOrDefault();
        }

        public List<CrackInfo> GetCracks(string status)
        {
            return _unitOfWork.CrackRepository
                .Get(filter: c => c.Status.Equals(status), includeProperties: "Censor,UpdateUser")
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)))
                .OrderByDescending(c => c.Created)
                .ThenBy(c => c.Status)
                .ThenBy(c => c.Severity)
                .ThenBy(c => c.CrackId)
                .Select(c => _mapper.Map<CrackInfo>(c))
                .ToList();
        }

        public List<CrackInfo> GetCracks()
        {
            return _unitOfWork.CrackRepository
                .Get(filter: c => !c.Status.Equals(CrackStatus.DetectedFailed), includeProperties: "Censor,UpdateUser")
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)))
                .OrderByDescending(c => c.Created)
                .ThenBy(c => c.Status)
                .ThenBy(c => c.Severity)
                .ThenBy(c => c.CrackId)
                .Select(c => _mapper.Map<CrackInfo>(c))
                .ToList();
        }

        public List<CrackInfo> GetCracksIgnore(string status)
        {
            return _unitOfWork.CrackRepository
                .Get(filter: c => !c.Status.Equals(CrackStatus.DetectedFailed) && !c.Status.Equals(status), includeProperties: "Censor,UpdateUser")
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)))
                .OrderByDescending(c => c.Created)
                .ThenBy(c => c.Status)
                .ThenBy(c => c.Severity)
                .ThenBy(c => c.CrackId)
                .Select(c => _mapper.Map<CrackInfo>(c))
                .ToList();
        }

        public int GetCracksCount()
        {
            return _unitOfWork.CrackRepository
                .Get(filter: c => !c.Status.Equals(CrackStatus.DetectedFailed), includeProperties: "Censor,UpdateUser")
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole))).Count();
        }

        public async Task<bool> Update(CrackBasicInfo crackBasicInfo, int id)
        {
            Crack crack = await _unitOfWork.CrackRepository.GetSingle(filter: c => !c.Status.Equals(CrackStatus.DetectedFailed) && c.CrackId.Equals(id));
            if (crackBasicInfo != null && crack != null)
            {
                crack.Description = crackBasicInfo.Description;
                if (!string.IsNullOrEmpty(crackBasicInfo.Position))
                    crack.Position = crackBasicInfo.Position;
                if (!string.IsNullOrEmpty(crackBasicInfo.Severity))
                    crack.Severity = crackBasicInfo.Severity;
                crack.Status = crackBasicInfo.Status;
                if (string.IsNullOrEmpty(crack.CensorId))
                    crack.CensorId = _userData.UserId;
                crack.UpdateUserId = _userData.UserId;
            }
            _unitOfWork.CrackRepository.Update(crack);
            return await _unitOfWork.Save() != 0;
        }

        public List<ChartValue> GetCracksCountBySeverity(int period, int year, int[] locationIds)
        {
            ValueTuple<int, int> periodTuple = MyUtils.GetMonthValue(period);
            var query = _unitOfWork.CrackRepository
                .Get(filter: c => !c.Status.Equals(CrackStatus.DetectedFailed))
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => c.Flight.RecordDate.Year.Equals(year)
                    && c.Flight.RecordDate.Month >= periodTuple.Item1
                    && c.Flight.RecordDate.Month <= periodTuple.Item2
                    && ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)));
            if (locationIds != null && locationIds.Length > 0)
                query = query.Where(c => locationIds.Contains(c.Flight.LocationId));
            return query.GroupBy(c => c.Severity)
                .Where(c => c.Key != null)
                .Select(c => new ChartValue { 
                    Value = c.Count(),
                    Key = c.Key
                }).ToList();
        }

        public List<ChartValue> GetCracksAssessmentCount(int period, int year, int[] locationIds)
        {
            ValueTuple<int, int> periodTuple = MyUtils.GetMonthValue(period);
            List<ChartValue> result = new();
            foreach (ValueTuple<int, int> rank in APIConstants.CrackAssessmentRank)
            {
                var query = _unitOfWork.CrackRepository
                .Get(filter: c => c.AssessmentResult > 0)
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => c.Flight.RecordDate.Year.Equals(year)
                    && c.Flight.RecordDate.Month >= periodTuple.Item1
                    && c.Flight.RecordDate.Month <= periodTuple.Item2
                    && c.AssessmentResult > rank.Item1
                    && c.AssessmentResult <= rank.Item2
                    && ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)));
                if (locationIds != null && locationIds.Length > 0)
                    query = query.Where(c => locationIds.Contains(c.Flight.LocationId));
                int value = query.Count();
                result.Add( new() {
                    Key = rank.Item1.ToString() + "-" + rank.Item2.ToString(),
                    Value = value
                });
            }
            return result;
        }

        public int GetCracksCountByStatus(string status, int period, int year, int[] locationIds)
        {
            ValueTuple<int, int> periodTuple = MyUtils.GetMonthValue(period);
            var query = _unitOfWork.CrackRepository
                .Get(filter: c => c.Status.Equals(status))
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => c.Flight.RecordDate.Year.Equals(year)
                    && c.Flight.RecordDate.Month >= periodTuple.Item1
                    && c.Flight.RecordDate.Month <= periodTuple.Item2
                    && ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)));
            if (locationIds != null && locationIds.Length > 0)
                query = query.Where(c => locationIds.Contains(c.Flight.LocationId));
            return query.Count();
        }

        public string GetMostCracksLocation(int period, int year)
        {
            ValueTuple<int, int> periodTuple = MyUtils.GetMonthValue(period);
            var query = _unitOfWork.CrackRepository
                .Get(filter: c => !c.Status.Equals(CrackStatus.DetectedFailed))
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => c.Flight.RecordDate.Year.Equals(year)
                    && c.Flight.RecordDate.Month >= periodTuple.Item1
                    && c.Flight.RecordDate.Month <= periodTuple.Item2
                    && ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)))
                .GroupBy(c => new
                {
                    c.Flight.LocationId,
                    c.Flight.Location.Name
                });
            return query.Where(c => !query.Any(c2 => c2.Count() > c.Count()))
                .Select(c => c.Key.Name).FirstOrDefault();
        }

        public List<ChartValue> GetCracksCountByStatus(int period, int year, int[] locationIds)
        {
            ValueTuple<int, int> periodTuple = MyUtils.GetMonthValue(period);
            var query = _unitOfWork.CrackRepository
                .Get(filter: c => !c.Status.Equals(CrackStatus.DetectedFailed))
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => c.Flight.RecordDate.Year.Equals(year)
                    && c.Flight.RecordDate.Month >= periodTuple.Item1
                    && c.Flight.RecordDate.Month <= periodTuple.Item2
                    && ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)));
            if (locationIds != null && locationIds.Length > 0)
                query = query.Where(c => locationIds.Contains(c.Flight.LocationId));
            return query.GroupBy(c => c.Status)
                .Select(c => new ChartValue { 
                    Key = c.Key,
                    Value = c.Count()
                }).ToList();
        }

        public List<ChartValueArray> GetCracksByLocationAndSeverity(int year, int locationId)
        {
            string[] customOrders = new string[] {"Low", "Medium", "High"};
            var query = _unitOfWork.CrackRepository
                .Get(filter: c => c.Status.Equals(CrackStatus.Fixed))
                .Include(c => c.Flight).ThenInclude(f => f.Location)
                .Where(c => c.Flight.RecordDate.Year.Equals(year)
                    && locationId.Equals(c.Flight.LocationId)
                    && ((_userData.LocationIds.Contains(c.Flight.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)));
            return query.GroupBy(c => new { Severity = c.Severity, Period = c.Created.Month/4 + 1 })
                .Select(c => new
                {
                    Severity = c.Key.Severity,
                    Period = c.Key.Period,
                    Count = c.Count()
                })
                .ToList()
                .OrderBy(ch => Array.IndexOf(customOrders, ch.Severity))
                .GroupBy(c => c.Period)
                .Select(c => new ChartValueArray
                {
                    Key = new DateTime(2010, (((c.Key - 1) * 4) + 1), 1).ToString("MMM") + "-" + new DateTime(2010, c.Key * 4, 1).ToString("MMM"),
                    Values = c.Select(c => c.Count).ToArray()
                })
                .ToList();
        }
    }
}