using CapstoneBE.Models;
using System.Collections.Generic;

namespace CapstoneBE.Repositories.PushNotifications
{
    public interface INotificationRepository
    {
        void DeleteRange(int[] ids);

        void CreateRange(List<PushNotification> pushNotifications);
    }
}