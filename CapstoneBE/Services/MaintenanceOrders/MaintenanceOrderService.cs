using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.Models.Custom.MaintenanceOrders;
using CapstoneBE.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorAPI.Utils;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Services.MaintenanceOrders
{
    public class MaintenanceOrderService : IMaintenanceOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetClaimsProvider _userData;
        private readonly IMapper _mapper;

        public MaintenanceOrderService(IUnitOfWork unitOfWork, IGetClaimsProvider userData, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userData = userData;
            _mapper = mapper;
        }

        public async Task<bool> AddToQueue(int crackId)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork.MaintenanceOrderRepository.GetQueue(_userData.UserId);
            if (maintenanceOrder == null)
                return await CreateQueue(crackId);
            return await UpdateQueue(crackId, maintenanceOrder);
        }

        public async Task<bool> ConfirmOrder(MaintenanceOrderBasicInfo maintenanceOrderBasicInfo)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork.MaintenanceOrderRepository.GetQueue(_userData.UserId);
            if (maintenanceOrder == null || maintenanceOrder.Cracks.Count <= 0)
                return false;
            if (maintenanceOrderBasicInfo != null)
            {
                using var tran = _unitOfWork.GetTransaction();
                try
                {
                    if (maintenanceOrderBasicInfo.MaintenanceWorkerId > 0)
                        maintenanceOrder.MaintenanceWorkerId = maintenanceOrderBasicInfo.MaintenanceWorkerId;
                    else return false;
                    maintenanceOrder.MaintenanceDate = maintenanceOrderBasicInfo.MaintenanceDate;
                    maintenanceOrder.Status = maintenanceOrderBasicInfo.Status;
                    maintenanceOrder.AssessorId = _userData.UserId;
                    await _unitOfWork.Save();
                    foreach (Crack crack in maintenanceOrder.Cracks)
                    {
                        crack.Status = CrackStatus.ScheduledForMaintenace;
                    }
                    await _unitOfWork.Save();
                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
            return false;
        }

        public async Task<bool> EvaluateOrder(MaintenanceOrderAssessmentInfo maintenanceOrderAssessmentInfo, int orderId)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork
                .MaintenanceOrderRepository.GetSingle(filter: mo => mo.MaintenanceOrderId.Equals(orderId)
                && mo.Status.Equals(MaintenanceOrderStatus.WaitingForMaintenance),
                includeProperties: "Cracks");
            if (maintenanceOrder == null || maintenanceOrder.Cracks.Count <= 0)
                return false;
            if (maintenanceOrderAssessmentInfo != null)
            {
                using var tran = _unitOfWork.GetTransaction();
                try
                {
                    if (maintenanceOrderAssessmentInfo.AssessmentResult > 0)
                        maintenanceOrder.AssessmentResult = maintenanceOrderAssessmentInfo.AssessmentResult;
                    else return false;
                    if (!string.IsNullOrEmpty(maintenanceOrderAssessmentInfo.Description))
                        maintenanceOrder.Description = maintenanceOrderAssessmentInfo.Description;
                    maintenanceOrder.Status = maintenanceOrderAssessmentInfo.Status;
                    maintenanceOrder.AssessorId = _userData.UserId;
                    await _unitOfWork.Save();
                    foreach (Crack crack in maintenanceOrder.Cracks)
                    {
                        crack.Status = CrackStatus.Fixed;
                    }
                    await _unitOfWork.Save();
                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
            return false;
        }

        public async Task<MaintenanceOrderInfo> GetById(int id)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork.MaintenanceOrderRepository
                .GetSingle(filter: mo => mo.MaintenanceOrderId.Equals(id),
                includeProperties: "Assessor,MaintenanceWorker,Cracks");
            return _mapper.Map<MaintenanceOrderInfo>(maintenanceOrder);
        }

        public List<MaintenanceOrderInfo> GetMaintenanceOrders()
        {
            return _unitOfWork.MaintenanceOrderRepository
                .Get(includeProperties: "Assessor,MaintenanceWorker,Cracks")
                .Select(mw => _mapper.Map<MaintenanceOrderInfo>(mw)).ToList();
        }

        public int GetMaintenanceOrdersCount()
        {
            return _unitOfWork.MaintenanceOrderRepository.Get()
                .Select(mw => _mapper.Map<MaintenanceOrderInfo>(mw)).Count();
        }

        public async Task<List<CrackInfo>> GetQueue()
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork.MaintenanceOrderRepository.GetQueue(_userData.UserId);
            if (maintenanceOrder == null)
                return null;
            return maintenanceOrder.Cracks.Select(c => _mapper.Map<CrackInfo>(c)).ToList();
        }

        public async Task<bool> RemoveFromQueue(int[] crackIds)
        {
            Crack[] cracks = _unitOfWork.CrackRepository.Get(filter: c => crackIds.Contains(c.CrackId)).ToArray();
            foreach (Crack crack in cracks)
            {
                crack.MaintenanceOrderId = null;
            }
            return await _unitOfWork.Save() == cracks.Length;
        }

        public async Task<bool> UpdateOrder(MaintenanceOrderBasicInfo maintenanceOrderBasicInfo, int orderId)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork
                .MaintenanceOrderRepository.GetSingle(filter: mo => mo.MaintenanceOrderId.Equals(orderId)
                && mo.Status.Equals(MaintenanceOrderStatus.WaitingForMaintenance),
                includeProperties: "Cracks");
            if (maintenanceOrder == null || maintenanceOrder.Cracks.Count <= 0)
                return false;
            if (maintenanceOrderBasicInfo != null)
            {
                if (maintenanceOrderBasicInfo.MaintenanceWorkerId > 0)
                    maintenanceOrder.MaintenanceWorkerId = maintenanceOrderBasicInfo.MaintenanceWorkerId;
                else return false;
                maintenanceOrder.MaintenanceDate = maintenanceOrderBasicInfo.MaintenanceDate;
                maintenanceOrder.Status = maintenanceOrderBasicInfo.Status;
                maintenanceOrder.AssessorId = _userData.UserId;
                return await _unitOfWork.Save() != 0;
            }
            return false;
        }

        private async Task<bool> CreateQueue(int crackId)
        {
            HashSet<Crack> cracks = _unitOfWork.CrackRepository.Get(filter: c => crackId.Equals(c.CrackId) && c.Status.Equals(CrackStatus.UnscheduledForMaintenace)).ToHashSet();
            if (cracks == null || cracks.Count <= 0)
                return false;
            MaintenanceOrder maintenanceOrder = new MaintenanceOrder
            {
                AssessorId = _userData.UserId,
                Cracks = cracks,
                Status = MaintenanceOrderStatus.WaitingForConfirm
            };
            _unitOfWork.MaintenanceOrderRepository.Create(maintenanceOrder);
            return await _unitOfWork.Save() != 0;
        }

        private async Task<bool> UpdateQueue(int crackId, MaintenanceOrder maintenanceOrder)
        {
            Crack crack = await _unitOfWork.CrackRepository.GetSingle(filter: c => crackId.Equals(c.CrackId) && c.Status.Equals(CrackStatus.UnscheduledForMaintenace));
            if (crack == null)
                return false;
            maintenanceOrder.Cracks.Add(crack);
            maintenanceOrder.AssessorId = _userData.UserId;
            return await _unitOfWork.Save() != 0;
        }
    }
}