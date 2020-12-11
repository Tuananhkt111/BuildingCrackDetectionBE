using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.Models.Custom.MaintenaceWorkers;
using CapstoneBE.Models.Custom.MaintenanceOrders;
using CapstoneBE.Services.MaintenanceOrders;
using CapstoneBE.Services.MaintenanceWorkers;
using CapstoneBE.Services.PushNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

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
        /// Delete notifications by <paramref name="ids"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/notifications
        /// </remarks>
        /// <param name="ids">Notification ids array</param>
        /// <returns>Message result</returns>
        /// <response code="200">If success, returns message "Delete notifications success"</response>
        /// <response code="404">If failed, returns message "Notifications doesn't exist"</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete([FromBody] int[] ids)
        {
            bool delResult = await _notificationService.Delete(ids);
            if (delResult)
                return Ok("Delete notifications success");
            return NotFound("Notifications doesn't exist");
        }
    }
}