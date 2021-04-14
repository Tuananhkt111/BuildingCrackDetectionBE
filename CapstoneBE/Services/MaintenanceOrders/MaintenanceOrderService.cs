using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.Models.Custom.MaintenanceOrders;
using CapstoneBE.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneBE.Utils;
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

        public async Task<bool> AddToQueue(int[] crackIds)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork.MaintenanceOrderRepository.GetQueue(_userData.UserId);
            if (maintenanceOrder == null)
                return await CreateQueue(crackIds);
            return await UpdateQueue(crackIds, maintenanceOrder);
        }

        public async Task<int> ConfirmOrder(MaintenanceOrderBasicInfo maintenanceOrderBasicInfo)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork.MaintenanceOrderRepository.GetQueue(_userData.UserId);
            if (maintenanceOrder == null || maintenanceOrder.Cracks.Count <= 0)
                return 0;
            if (maintenanceOrderBasicInfo != null)
            {
                using var tran = _unitOfWork.GetTransaction();
                try
                {
                    if (maintenanceOrderBasicInfo.MaintenanceWorkerId > 0)
                        maintenanceOrder.MaintenanceWorkerId = maintenanceOrderBasicInfo.MaintenanceWorkerId;
                    else return 0;
                    maintenanceOrder.MaintenanceDate = maintenanceOrderBasicInfo.MaintenanceDate;
                    maintenanceOrder.MaintenanceExpense = maintenanceOrderBasicInfo.MaintenanceExpense;
                    maintenanceOrder.Status = maintenanceOrderBasicInfo.Status;
                    maintenanceOrder.CreateUserId = _userData.UserId;
                    maintenanceOrder.UpdateUserId = _userData.UserId;
                    await _unitOfWork.Save();
                    foreach (Crack crack in maintenanceOrder.Cracks)
                    {
                        crack.Status = CrackStatus.ScheduledForMaintenace;
                    }
                    await _unitOfWork.Save();
                    tran.Commit();
                    return maintenanceOrder.MaintenanceOrderId;
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
            return 0;
        }

        public async Task<int> EvaluateOrder(MaintenanceOrderAssessmentInfo maintenanceOrderAssessmentInfo, int orderId)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork
                .MaintenanceOrderRepository.GetSingle(filter: mo => mo.MaintenanceOrderId.Equals(orderId)
                && mo.Status.Equals(MaintenanceOrderStatus.WaitingForMaintenance),
                includeProperties: "Cracks");
            if (maintenanceOrder == null || maintenanceOrder.Cracks.Count <= 0)
                return 0;
            if (maintenanceOrderAssessmentInfo != null)
            {
                using var tran = _unitOfWork.GetTransaction();
                try
                {
                    if (maintenanceOrderAssessmentInfo.AssessmentResult > 0)
                        maintenanceOrder.AssessmentResult = maintenanceOrderAssessmentInfo.AssessmentResult;
                    else return 0;
                    maintenanceOrder.Description = maintenanceOrderAssessmentInfo.Description;
                    maintenanceOrder.Status = maintenanceOrderAssessmentInfo.Status;
                    maintenanceOrder.AssessorId = _userData.UserId;
                    maintenanceOrder.AssessmentDate = DateTime.UtcNow;
                    await _unitOfWork.Save();
                    foreach (CrackAssessmentInfo crackAssessment in maintenanceOrderAssessmentInfo.CrackAssessments)
                    {
                        Crack crack = await _unitOfWork.CrackRepository.GetById(crackAssessment.CrackId);
                        crack.AssessmentResult = crackAssessment.AssessmentResult;
                        crack.AssessmentDescription = crackAssessment.AssessmentDescription;
                        crack.Status = crackAssessment.Status;
                    }
                    await _unitOfWork.Save();
                    tran.Commit();
                    return maintenanceOrder.MaintenanceOrderId;
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
            return 0;
        }

        public async Task<MaintenanceOrderInfo> GetById(int id)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork.MaintenanceOrderRepository
                .GetSingle(filter: mo => mo.MaintenanceOrderId.Equals(id)
                    && ((_userData.LocationIds.Contains(mo.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)),
                includeProperties: "Assessor,MaintenanceWorker,Cracks,Location,CreateUser,UpdateUser");
            return _mapper.Map<MaintenanceOrderInfo>(maintenanceOrder);
        }

        public List<MaintenanceOrderInfo> GetMaintenanceOrders()
        {
            return _unitOfWork.MaintenanceOrderRepository
                .Get(filter: mo => (_userData.LocationIds.Contains(mo.LocationId)
                    && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole),
                    includeProperties: "Assessor,MaintenanceWorker,Cracks,Location,CreateUser,UpdateUser")
                .OrderByDescending(mo => mo.Created)
                .Select(mo => _mapper.Map<MaintenanceOrderInfo>(mo)).ToList();
        }

        public List<MaintenanceOrderInfo> GetMaintenanceOrders(string status)
        {
            return _unitOfWork.MaintenanceOrderRepository
                .Get(filter: mo => mo.Status.Equals(status) && (_userData.LocationIds.Contains(mo.LocationId)
                    && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole),
                    includeProperties: "Assessor,MaintenanceWorker,Cracks,Location,CreateUser,UpdateUser")
                .OrderByDescending(mo => mo.MaintenanceDate)
                .Select(mo => _mapper.Map<MaintenanceOrderInfo>(mo)).ToList();
        }

        public int GetMaintenanceOrdersCount()
        {
            return _unitOfWork.MaintenanceOrderRepository
                .Get(filter: mo => (_userData.LocationIds.Contains(mo.LocationId)
                    && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole))
                .Count();
        }

        public async Task<List<CrackSubDetailsInfo>> GetQueue()
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork.MaintenanceOrderRepository.GetQueue(_userData.UserId);
            if (maintenanceOrder == null)
                return null;
            return maintenanceOrder.Cracks.Select(c => _mapper.Map<CrackSubDetailsInfo>(c)).ToList();
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

        public async Task<int> UpdateOrder(MaintenanceOrderBasicInfo maintenanceOrderBasicInfo, int orderId)
        {
            MaintenanceOrder maintenanceOrder = await _unitOfWork
                .MaintenanceOrderRepository.GetSingle(filter: mo => mo.MaintenanceOrderId.Equals(orderId)
                && mo.Status.Equals(MaintenanceOrderStatus.WaitingForMaintenance),
                includeProperties: "Cracks");
            if (maintenanceOrder == null || maintenanceOrder.Cracks.Count <= 0)
                return 0;
            if (maintenanceOrderBasicInfo != null)
            {
                if (maintenanceOrderBasicInfo.MaintenanceWorkerId > 0)
                    maintenanceOrder.MaintenanceWorkerId = maintenanceOrderBasicInfo.MaintenanceWorkerId;
                else return 0;
                maintenanceOrder.MaintenanceDate = maintenanceOrderBasicInfo.MaintenanceDate;
                maintenanceOrder.MaintenanceExpense = maintenanceOrderBasicInfo.MaintenanceExpense;
                maintenanceOrder.Status = maintenanceOrderBasicInfo.Status;
                maintenanceOrder.UpdateUserId = _userData.UserId;
                bool result = await _unitOfWork.Save() != 0;
                return result ? maintenanceOrder.MaintenanceOrderId : 0;
            }
            return 0;
        }

        private async Task<bool> CreateQueue(int[] crackIds)
        {
            HashSet<Crack> cracks = _unitOfWork.CrackRepository.Get(filter: c => crackIds.Contains(c.CrackId) && c.Status.Equals(CrackStatus.UnscheduledForMaintenace)).ToHashSet();
            if (cracks == null || cracks.Count <= 0)
                return false;
            MaintenanceOrder maintenanceOrder = new MaintenanceOrder
            {
                CreateUserId = _userData.UserId,
                Cracks = cracks,
                Status = MaintenanceOrderStatus.WaitingForConfirm,
                LocationId = _userData.LocationIds[0]
            };
            _unitOfWork.MaintenanceOrderRepository.Create(maintenanceOrder);
            return await _unitOfWork.Save() != 0;
        }

        public List<ChartValue> GetMaintenanceOrdersCountByStatus(int period, int year, int[] locationIds)
        {
            ValueTuple<int, int> periodTuple = MyUtils.GetMonthValue(period);
            var query = _unitOfWork.MaintenanceOrderRepository
                .Get(filter: mo => !mo.Status.Equals(MaintenanceOrderStatus.WaitingForConfirm))
                .Where(mo => mo.MaintenanceDate.Year.Equals(year)
                    && mo.MaintenanceDate.Month >= periodTuple.Item1
                    && mo.MaintenanceDate.Month <= periodTuple.Item2
                    && ((_userData.LocationIds.Contains(mo.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)));
            if (locationIds != null && locationIds.Length > 0)
                query = query.Where(mo => locationIds.Contains(mo.LocationId));
            return query.GroupBy(mo => mo.Status)
                .Select(c => new ChartValue
                {
                    Key = c.Key,
                    Value = c.Count()
                }).ToList();
        }

        private async Task<bool> UpdateQueue(int[] crackIds, MaintenanceOrder maintenanceOrder)
        {
            List<Crack> cracks = _unitOfWork.CrackRepository.Get(filter: c => crackIds.Contains(c.CrackId) && c.Status.Equals(CrackStatus.UnscheduledForMaintenace)).ToList();
            if (cracks == null || cracks.Count == 0)
                return false;
            foreach (var crack in cracks)
            {
                maintenanceOrder.Cracks.Add(crack);
            }
            maintenanceOrder.CreateUserId = _userData.UserId;
            return await _unitOfWork.Save() != 0;
        }

        public List<ChartValueFloat> GetMaintenanceOrdersExpense(int year, int locationId)
        {
            var query = _unitOfWork.MaintenanceOrderRepository
                .Get(filter: mo => !mo.Status.Equals(MaintenanceOrderStatus.WaitingForConfirm))
                .Where(mo => mo.MaintenanceDate.Year.Equals(year)
                    && locationId.Equals(mo.LocationId)
                    && ((_userData.LocationIds.Contains(mo.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)));
            return query.GroupBy(mo => mo.MaintenanceDate.Month / 4 + 1)
                .Select(mo => new ChartValueFloat
                {
                    Key = new DateTime(2010, (((mo.Key - 1) * 4) + 1), 1).ToString("MMM") + "-" + new DateTime(2010, mo.Key * 4, 1).ToString("MMM"),
                    Value = mo.Sum(mo => mo.MaintenanceExpense)
                }).ToList();
        }

        public float GetMaintenanceOrdersExpenseTotal(int year, int locationId)
        {
            return _unitOfWork.MaintenanceOrderRepository
                .Get(filter: mo => !mo.Status.Equals(MaintenanceOrderStatus.WaitingForConfirm))
                .Where(mo => mo.MaintenanceDate.Year.Equals(year)
                    && locationId.Equals(mo.LocationId)
                    && ((_userData.LocationIds.Contains(mo.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)))
                .Sum(mo => mo.MaintenanceExpense);
        }

        public int GetMaintenanceOrdersCount(int year, int locationId)
        {
            return _unitOfWork.MaintenanceOrderRepository
                .Get(filter: mo => !mo.Status.Equals(MaintenanceOrderStatus.WaitingForConfirm))
                .Where(mo => mo.MaintenanceDate.Year.Equals(year)
                    && locationId.Equals(mo.LocationId)
                    && ((_userData.LocationIds.Contains(mo.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)))
                .Count();
        }

        public double GetMaintenanceOrdersAssessmentAverage(int year, int locationId)
        {
            return _unitOfWork.MaintenanceOrderRepository
                .Get(filter: mo => mo.Status.Equals(MaintenanceOrderStatus.Completed))
                .Where(mo => mo.MaintenanceDate.Year.Equals(year)
                    && locationId.Equals(mo.LocationId)
                    && ((_userData.LocationIds.Contains(mo.LocationId) && !_userData.Role.Equals(Roles.AdminRole))
                    || _userData.Role.Equals(Roles.AdminRole)))
                .Average(mo => mo.AssessmentResult);
        }
    }
}