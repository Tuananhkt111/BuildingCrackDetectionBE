using CapstoneBE.Models;
using CapstoneBE.UnitOfWorks;
using FirebaseAdmin.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneBE.Utils;
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

        public List<Message> GetMessages(CapstoneBEUser sender, CapstoneBEUser receiver, string messageType, int? orderId = null)
        {
            List<Message> messages = new();
            switch (messageType)
            {
                case MessageTypes.AdminUpdateInfo:
                case MessageTypes.SystemFinishedDetection:
                    if (!string.IsNullOrEmpty(receiver.FcmTokenM))
                        messages.Add(new Message()
                        {
                            Token = receiver.FcmTokenM,
                            Notification = GetNotification(sender, messageType)
                        });
                    if (!string.IsNullOrEmpty(receiver.FcmTokenW))
                        messages.Add(new Message()
                        {
                            Token = receiver.FcmTokenW,
                            Notification = GetNotification(sender, messageType)
                        });
                    break;

                case MessageTypes.StaffCreateOrder:
                case MessageTypes.StaffUpdateOrder:
                case MessageTypes.StaffEvaluateOrder:
                    if (orderId == null || orderId == 0)
                        return null;
                    messages.Add(new Message()
                    {
                        Data = new Dictionary<string, string>()
                        {
                            { "orderId", orderId.ToString() }
                        },
                        Token = receiver.FcmTokenW,
                        Notification = GetNotification(sender, messageType)
                    });
                    break;
            }
            return messages.Count > 0 ? messages : null;
        }

        public Notification GetNotification(CapstoneBEUser sender, string messageType)
        {
            Notification notification = messageType switch
            {
                MessageTypes.AdminUpdateInfo => new Notification
                {
                    Title = "Administrator has updated your profile",
                    Body = "Review your personal information in Profile tab"
                },
                MessageTypes.SystemFinishedDetection => new Notification
                {
                    Title = "System has finished detecting cracks",
                    Body = "Detection results are shown in 'Not Verify' tab"
                },
                MessageTypes.StaffCreateOrder => new Notification
                {
                    Title = "Staff " + sender.Name + " has created a repair record in area "
                        + _unitOfWork.LocationHistoryRepository.GetLocationOfStaffById(sender.Id).Result?.Name,
                    Body = "Tap to view details"
                },
                MessageTypes.StaffUpdateOrder => new Notification
                {
                    Title = "Staff " + sender.Name + " has updated a repair record in area "
                        + _unitOfWork.LocationHistoryRepository.GetLocationOfStaffById(sender.Id).Result?.Name,
                    Body = "Tap to view details"
                },
                MessageTypes.StaffEvaluateOrder => new Notification
                {
                    Title = "Staff " + sender.Name + " has evaluated a repair record in area "
                        + _unitOfWork.LocationHistoryRepository.GetLocationOfStaffById(sender.Id).Result?.Name,
                    Body = "Tap to view details"
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
                    List<Message> messageList = GetMessages(sender, receiver, messageType, orderId);
                    messages.AddRange(messageList);
                    pushNotifications.Add(new PushNotification
                    {
                        SenderId = senderId,
                        ReceiverId = receiverId,
                        Title = messageList[0].Notification.Title,
                        Body = messageList[0].Notification.Body,
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