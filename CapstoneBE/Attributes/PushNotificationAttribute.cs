using CapstoneBE.Services.PushNotifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
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
               InitializeProperties(context.HttpContext.Request, okObjectResult?.Value);
               INotificationService notificationService = context.HttpContext.RequestServices.GetRequiredService<INotificationService>();
               await notificationService.SendNotifications(SenderId, ReceiverIds, MessageType, OrderId);
           }
       }

       private bool InitializeProperties(HttpRequest request, object okOject)
       {
           switch (MessageType)
           {
               case MessageTypes.AdminUpdateInfo:
                   if (okOject is int okInteger)
                   {
                       if (okInteger > 0)
                       {
                           SenderId = null;
                           OrderId = null;
                           ReceiverIds = new string[] { "a" };
                           return true;
                       }
                   }
                   return false;
               //case MessageTypes.SystemFinishedDetection:
               case MessageTypes.StaffCreateOrder:
               case MessageTypes.StaffUpdateOrder:
               case MessageTypes.StaffEvaluateOrder:
                   return true;

               default: return false;
           }
       }
   }
}