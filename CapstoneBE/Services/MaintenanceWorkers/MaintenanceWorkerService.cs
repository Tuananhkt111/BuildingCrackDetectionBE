using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.MaintenaceWorkers;
using CapstoneBE.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Services.MaintenanceWorkers
{
    public class MaintenanceWorkerService : IMaintenanceWorkerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaintenanceWorkerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(MaintenanceWorkerBasicInfo maintenanceWorkerBasicInfo)
        {
            MaintenanceWorker worker = _mapper.Map<MaintenanceWorker>(maintenanceWorkerBasicInfo);
            _unitOfWork.MaintenanceWorkerRepository.Create(worker);
            return await _unitOfWork.Save() != 0;
        }

        public async Task<bool> Delete(int id)
        {
            bool isRemovable = !_unitOfWork.MaintenanceOrderRepository.Get(filter: mo => mo.Status.Equals(MaintenanceOrderStatus.WaitingForMaintenance)
                && mo.MaintenanceWorkerId.Equals(id)).Any();
            if (!isRemovable)
                return false;
            _unitOfWork.MaintenanceWorkerRepository.Delete(id).Wait();
            return await _unitOfWork.Save() != 0;
        }

        public async Task<MaintenanceWorkerInfo> GetById(int id)
        {
            MaintenanceWorker worker = await _unitOfWork.MaintenanceWorkerRepository.GetById(id);
            return _mapper.Map<MaintenanceWorkerInfo>(worker);
        }

        public List<MaintenanceWorkerInfo> GetMaintenanceWorkers()
        {
            return _unitOfWork.MaintenanceWorkerRepository.Get(filter: mw => !mw.IsDel).Select(mw => _mapper.Map<MaintenanceWorkerInfo>(mw)).ToList();
        }

        public int GetMaintenanceWorkersCount()
        {
            return _unitOfWork.MaintenanceWorkerRepository.Get(filter: mw => !mw.IsDel).Select(mw => _mapper.Map<MaintenanceWorkerInfo>(mw)).Count();
        }

        public async Task<int> Update(MaintenanceWorkerBasicInfo maintenanceWorkerBasicInfo, int id)
        {
            await _unitOfWork.MaintenanceWorkerRepository.UpdateBasicInfo(maintenanceWorkerBasicInfo, id);
            return await _unitOfWork.Save();
        }
    }
}