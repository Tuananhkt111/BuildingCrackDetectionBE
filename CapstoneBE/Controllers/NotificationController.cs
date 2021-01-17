using CapstoneBE.Models;
using CapstoneBE.Services.PushNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Controllers
{
    [Authorize]
    [Route("api/v1/notifications")]
    [ApiVersion("1.0")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Mark as read notifications by <paramref name="ids"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/notifications
        /// </remarks>
        /// <param name="ids">Notification ids array</param>
        /// <returns>Message result</returns>
        /// <response code="200">If success, returns message "Mark as read notifications success"</response>
        /// <response code="404">If failed, returns message "Notifications doesn't exist"</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete([FromBody] int[] ids)
        {
            bool delResult = await _notificationService.Delete(ids);
            if (delResult)
                return Ok("Delete notifications success");
            return NotFound("Notifications doesn't exist");
        }

        /// <summary>
        /// Get list of notifications {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/notifications?isRead=true
        /// </remarks>
        /// <param name="isRead">Status of notification</param>
        /// <returns>List of notifications</returns>
        /// <response code="200">Returns list of notifications</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PushNotification>> GetNotifications(bool? isRead = null)
        {
            List<PushNotification> notifications;
            if (isRead == null)
                notifications = _notificationService.GetPushNotifications();
            else
                notifications = _notificationService.GetPushNotifications(isRead);
            if (notifications != null)
                return Ok(notifications);
            return NotFound();
        }
    }
}