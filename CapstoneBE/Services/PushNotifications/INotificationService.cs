using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Locations;
using FirebaseAdmin.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.PushNotifications
{
    public interface INotificationService
    {
        Task<bool> Delete(int[] ids);

        Message GetMessage(CapstoneBEUser sender, CapstoneBEUser receiver, string messageType, int? orderId = null);

        Notification GetNotification(CapstoneBEUser sender, string messageType);

        Task<bool> CreateRange(List<PushNotification> pushNotifications);

        Task<bool> SendNotifications(string senderId, string[] receiverIds, string messageType,
            int? orderId = null);

        List<PushNotification> GetPushNotifications();
    }
}