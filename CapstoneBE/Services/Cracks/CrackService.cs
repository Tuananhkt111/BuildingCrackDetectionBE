using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorAPI.Utils;
using static CapstoneBE.Utils.APIConstants;

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
                if (!string.IsNullOrEmpty(crackBasicInfo.Description))
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
    }
}