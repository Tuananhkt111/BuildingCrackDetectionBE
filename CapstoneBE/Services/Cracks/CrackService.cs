using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorAPI.Utils;

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

        public async Task<bool> Create(CrackCreate crackCreate)
        {
            Crack crack = _mapper.Map<Crack>(crackCreate);
            crack.ReporterId = _userData.UserId;
            crack.LocationId = _userData.LocationIds.FirstOrDefault();
            _unitOfWork.CrackRepository.Create(crack);
            return await _unitOfWork.Save() != 0;
        }

        public async Task<bool> CreateRange(CrackCreate[] crackCreates)
        {
            Crack[] cracks = crackCreates.Select(c => _mapper.Map<Crack>(c)).ToArray();
            foreach (Crack crack in cracks)
            {
                crack.ReporterId = _userData.UserId;
                crack.LocationId = _userData.LocationIds.FirstOrDefault();
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
            Crack crack = await _unitOfWork.CrackRepository.GetById(id);
            return _mapper.Map<CrackInfo>(crack);
        }

        public List<CrackInfo> GetCracks()
        {
            return _unitOfWork.CrackRepository.Get().Select(c => _mapper.Map<CrackInfo>(c)).ToList();
        }

        public int GetCracksCount()
        {
            return _unitOfWork.CrackRepository.Get().Count();
        }

        public async Task<bool> Update(CrackBasicInfo crackBasicInfo, int id)
        {
            Crack crack = await _unitOfWork.CrackRepository.GetById(id);
            if (crackBasicInfo != null && crack != null)
            {
                if (!string.IsNullOrEmpty(crackBasicInfo.Description))
                    crack.Description = crackBasicInfo.Description;
                if (!string.IsNullOrEmpty(crackBasicInfo.Image))
                    crack.Image = crackBasicInfo.Image;
                if (!string.IsNullOrEmpty(crackBasicInfo.Position))
                    crack.Position = crackBasicInfo.Position;
                if (!string.IsNullOrEmpty(crackBasicInfo.Severity))
                    crack.Severity = crackBasicInfo.Severity;
                crack.Status = crackBasicInfo.Status;
                crack.ReporterId = _userData.UserId;
                crack.LocationId = _userData.LocationIds.FirstOrDefault();
            }
            _unitOfWork.CrackRepository.Update(crack);
            return await _unitOfWork.Save() != 0;
        }
    }
}