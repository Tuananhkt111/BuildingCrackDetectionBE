using CapstoneBE.Services.PushNotifications;
using CapstoneBE.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TranslatorAPI.Utils;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PushNotificationAttribute : Attribute, IAsyncActionFilter
    {
        public string MessageType { get; set; }
        public string SenderId { get; set; }
        public string[] ReceiverIds { get; set; }
        public int? OrderId { get; set; }

        public PushNotificationAttribute(string messageType)
        {
            MessageType = messageType;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();
            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                bool flag = InitializeProperties(context, okObjectResult?.Value);
                if (flag)
                {
                    INotificationService notificationService = context.HttpContext.RequestServices.GetRequiredService<INotificationService>();
                    await notificationService.SendNotifications(SenderId, ReceiverIds, MessageType, OrderId);
                }
            }
        }

        private bool InitializeProperties(ActionExecutingContext context, object okOject)
        {
            switch (MessageType)
            {
                case MessageTypes.AdminUpdateInfo:
                    if (okOject is int okInteger)
                    {
                        if (okInteger > 0)
                        {
                            string path = context.HttpContext.Request.Path.ToString();
                            int startIndex = path.LastIndexOf('/');
                            string userId = path.Substring(startIndex + 1);
                            SenderId = null;
                            OrderId = null;
                            ReceiverIds = new string[] { userId };
                            return true;
                        }
                    }
                    return false;
                //case MessageTypes.SystemFinishedDetection:
                case MessageTypes.StaffCreateOrder:
                case MessageTypes.StaffUpdateOrder:
                case MessageTypes.StaffEvaluateOrder:
                    if (okOject is int orderId)
                    {
                        if (orderId > 0)
                        {
                            IGetClaimsProvider userData = context.HttpContext.RequestServices.GetRequiredService<IGetClaimsProvider>();
                            IUserService userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            string receiverId = userService.GetManagerIdByLocationId(userData.LocationIds[0]);
                            SenderId = userData.UserId;
                            OrderId = orderId;
                            ReceiverIds = new string[] { receiverId };
                            return true;
                        }
                    }
                    return false;

                default: return false;
            }
        }
    }
}