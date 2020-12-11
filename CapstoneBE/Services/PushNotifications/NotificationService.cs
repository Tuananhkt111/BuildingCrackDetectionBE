using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Locations;
using CapstoneBE.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Services.PushNotifications
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(int[] ids)
        {
            _unitOfWork.NotificationRepository.DeleteRange(ids);
            return await _unitOfWork.Save() != 0;
        }
    }
}