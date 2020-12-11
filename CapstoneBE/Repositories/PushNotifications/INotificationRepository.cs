using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.PushNotifications
{
    public interface INotificationRepository
    {
        void DeleteRange(int[] ids);
    }
}