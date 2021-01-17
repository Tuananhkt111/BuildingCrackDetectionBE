using CapstoneBE.Models;
using CapstoneBE.UnitOfWorks;
using FirebaseAdmin.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorAPI.Utils;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Services.PushNotifications
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetClaimsProvider _userData;

        public NotificationService(IUnitOfWork unitOfWork, IGetClaimsProvider userData)
        {
            _unitOfWork = unitOfWork;
            _userData = userData;
        }

        public async Task<bool> CreateRange(List<PushNotification> pushNotifications)
        {
            _unitOfWork.NotificationRepository.CreateRange(pushNotifications);
            return await _unitOfWork.Save() == pushNotifications.Count;
        }

        public async Task<bool> Delete(int[] ids)
        {
            _unitOfWork.NotificationRepository.DeleteRange(ids);
            return await _unitOfWork.Save() == ids.Length;
        }

        public Message GetMessage(CapstoneBEUser sender, CapstoneBEUser receiver, string messageType, int? orderId = null)
        {
            switch (messageType)
            {
                case MessageType.AdminUpdateInfo:
                case MessageType.SystemFinishedDetection:
                    return new Message()
                    {
                        Token = receiver.FcmToken,
                        Notification = GetNotification(sender, messageType)
                    };

                case MessageType.StaffCreateOrder:
                case MessageType.StaffUpdateOrder:
                case MessageType.StaffEvaluateOrder:
                    if (orderId == null || orderId == 0)
                        return null;
                    return new Message()
                    {
                        Data = new Dictionary<string, string>()
                        {
                            { "orderId", orderId.ToString() }
                        },
                        Token = receiver.FcmToken,
                        Notification = GetNotification(sender, messageType)
                    };

                default: return null;
            }
        }

        public Notification GetNotification(CapstoneBEUser sender, string messageType)
        {
            Notification notification = messageType switch
            {
                MessageType.AdminUpdateInfo => new Notification
                {
                    Title = "Administrator has updated your profile",
                    Body = "Review your personal information in Profile tab."
                },
                MessageType.SystemFinishedDetection => new Notification
                {
                    Title = "System has finished detecting cracks",
                    Body = "Detection results are shown in 'Unconfirmed Cracks' tab."
                },
                MessageType.StaffCreateOrder => new Notification
                {
                    Title = "Staff " + sender.Name + " has created a maintenance order in location "
                        + _unitOfWork.LocationHistoryRepository.GetLocationOfStaffById(sender.Id).Result?.Name,
                    Body = "Tap to view details."
                },
                MessageType.StaffUpdateOrder => new Notification
                {
                    Title = "Staff " + sender.Name + " has updated a maintenance order in location "
                        + _unitOfWork.LocationHistoryRepository.GetLocationOfStaffById(sender.Id).Result?.Name,
                    Body = "Tap to view details."
                },
                MessageType.StaffEvaluateOrder => new Notification
                {
                    Title = "Staff " + sender.Name + " has evaluated a maintenance order in location "
                        + _unitOfWork.LocationHistoryRepository.GetLocationOfStaffById(sender.Id).Result?.Name,
                    Body = "Tap to view details."
                },
                _ => null
            };
            return notification;
        }

        public List<PushNotification> GetPushNotifications()
        {
            return _unitOfWork.NotificationRepository
                .Get(filter: n => n.ReceiverId.Equals(_userData.UserId))
                .OrderByDescending(n => n.Created)
                .ToList();
        }

        public List<PushNotification> GetPushNotifications(bool? isRead)
        {
            if (isRead == null)
                return null;
            return _unitOfWork.NotificationRepository
                .Get(filter: n => n.ReceiverId.Equals(_userData.UserId) && n.IsRead.Equals(isRead))
                .OrderByDescending(n => n.Created)
                .ToList();
        }

        public async Task<bool> SendNotifications(string senderId, string[] receiverIds, string messageType, int? orderId = null)
        {
            List<Message> messages = new List<Message>();
            List<PushNotification> pushNotifications = new List<PushNotification>();
            CapstoneBEUser sender = await _unitOfWork.UserRepository.GetById(senderId);
            foreach (string receiverId in receiverIds)
            {
                CapstoneBEUser receiver = await _unitOfWork.UserRepository.GetById(receiverId);
                if (receiver != null)
                {
                    Message message = GetMessage(sender, receiver, messageType, orderId);
                    messages.Add(message);
                    pushNotifications.Add(new PushNotification
                    {
                        SenderId = senderId,
                        ReceiverId = receiverId,
                        Title = message.Notification.Title,
                        Body = message.Notification.Body,
                        MessageType = messageType,
                        IsRead = false
                    });
                }
            }
            bool createResult = await CreateRange(pushNotifications);
            return (await FirebaseMessaging.DefaultInstance.SendAllAsync(messages)).FailureCount == 0 && createResult;
        }
    }
}