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
    }
}