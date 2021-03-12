﻿using CapstoneBE.Models;
using FirebaseAdmin.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.PushNotifications
{
    public interface INotificationService
    {
        Task<bool> Delete(int[] ids);

        List<Message> GetMessages(CapstoneBEUser sender, CapstoneBEUser receiver, string messageType, int? orderId = null);

        Notification GetNotification(CapstoneBEUser sender, string messageType);

        Task<bool> CreateRange(List<PushNotification> pushNotifications);

        Task<bool> SendNotifications(string senderId, string[] receiverIds, string messageType,
            int? orderId = null);

        List<PushNotification> GetPushNotifications();

        List<PushNotification> GetPushNotifications(bool? isRead);
    }
}