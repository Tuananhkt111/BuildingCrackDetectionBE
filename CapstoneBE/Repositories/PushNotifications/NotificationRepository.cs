using CapstoneBE.Data;
using CapstoneBE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.PushNotifications
{
    public class NotificationRepository : GenericRepository<PushNotification>, INotificationRepository
    {
        public NotificationRepository(CapstoneDbContext capstoneDbContext) : base(capstoneDbContext)
        {
        }

        public void CreateRange(List<PushNotification> pushNotifications)
        {
            _dbSet.AddRange(pushNotifications);
        }

        public void DeleteRange(int[] ids)
        {
            List<PushNotification> pushNotifications = Get(filter: n => ids.Contains(n.PushNotificationId)).ToList();
            if (pushNotifications.Count > 0)
            {
                foreach (PushNotification noti in pushNotifications)
                {
                    noti.IsRead = true;
                }
            }
        }
    }
}