using CapstoneBE.Attributes;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.Models.Custom.MaintenaceWorkers;
using CapstoneBE.Models.Custom.MaintenanceOrders;
using CapstoneBE.Services.MaintenanceOrders;
using CapstoneBE.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TranslatorAPI.Utils;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Controllers
{
    [Authorize]
    [Route("api/v1/maintenance-orders")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MaintenanceOrderController : ControllerBase
    {
        private readonly IMaintenanceOrderService _maintenanceOrderService;
        private readonly IUserService _userService;
        private readonly IGetClaimsProvider _userData;

        public MaintenanceOrderController(IMaintenanceOrderService maintenanceOrderService, IUserService userService, IGetClaimsProvider userData)
        {
            _maintenanceOrderService = maintenanceOrderService;
            _userService = userService;
            _userData = userData;
        }

        /// <summary>
        /// Add maintenance order queue {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/maintenance-orders/queue
        /// </remarks>
        /// <param name="crackIds">Crack ids</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Add maintenance order queue success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Add maintenance order queue failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost("queue")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddMaintenanceOrderQueue([FromBody] int[] crackIds)
        {
            if (crackIds == null || crackIds.Length == 0)
                return BadRequest("Invalid request");
            bool result = await _maintenanceOrderService.AddToQueue(crackIds);
            return result ? Ok("Add maintenance order queue success") : BadRequest("Add maintenance order queue failed");
        }

        /// <summary>
        /// Remove from maintenance order queue {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/maintenance-orders/queue
        /// </remarks>
        /// <param name="crackIds">An crack ids array</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Remove from maintenance order queue success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Remove from maintenance order queue failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpDelete("queue")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> RemoveFromMaintenanceOrderQueue([FromBody] int[] crackIds)
        {
            if (crackIds == null || crackIds.Length <= 0)
                return BadRequest("Invalid request");
            bool result = await _maintenanceOrderService.RemoveFromQueue(crackIds);
            return result ? Ok("Remove from maintenance order queue success") : BadRequest("Remove from maintenance order queue failed");
        }

        /// <summary>
        /// Confirm maintenance order {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/maintenance-orders/confirm
        /// </remarks>
        /// <param name="maintenanceOrderBasicInfo">An MaintenanceOrderBasicInfo object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns maintenance order id</response>
        /// <response code="400">
        /// <para>If failed, returns message "Confirm maintenance order failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost("confirm")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [PushNotification(MessageTypes.StaffCreateOrder)]
        public async Task<ActionResult<string>> ConfirmMaintenanceOrder([FromBody] MaintenanceOrderBasicInfo maintenanceOrderBasicInfo)
        {
            if (maintenanceOrderBasicInfo == null)
                return BadRequest("Invalid request");
            int result = await _maintenanceOrderService.ConfirmOrder(maintenanceOrderBasicInfo);
            return result > 0 ? Ok(result.ToString()) : BadRequest("Confirm maintenance order failed");
        }

        /// <summary>
        /// Evaluate maintenance order {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/maintenance-orders/2/evaluate
        /// </remarks>
        /// <param name="id">Maintenance Order Id</param>
        /// <param name="maintenanceOrderAssessmentInfo">An MaintenanceOrderAssessmentInfo object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Evaluate maintenance order success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Evaluate maintenance order failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost("{id}/evaluate")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [PushNotification(MessageTypes.StaffEvaluateOrder)]
        public async Task<ActionResult<string>> EvaluateMaintenanceOrder(int id, [FromBody] MaintenanceOrderAssessmentInfo maintenanceOrderAssessmentInfo)
        {
            if (maintenanceOrderAssessmentInfo == null || id == 0)
                return BadRequest("Invalid request");
            int result = await _maintenanceOrderService.EvaluateOrder(maintenanceOrderAssessmentInfo, id);
            return result > 0 ? Ok("Evaluate maintenance order success") : BadRequest("Evaluate maintenance order failed");
        }

        /// <summary>
        /// Update maintenance order {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/maintenance-orders/2
        /// </remarks>
        /// <param name="id">Maintenance Order Id</param>
        /// <param name="maintenanceOrderBasicInfo">An MaintenanceOrderBasicInfo object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Update maintenance order success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Update maintenance order failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost("{id}")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [PushNotification(MessageTypes.StaffUpdateOrder)]
        public async Task<ActionResult<string>> UpdateMaintenanceOrder(int id, [FromBody] MaintenanceOrderBasicInfo maintenanceOrderBasicInfo)
        {
            if (maintenanceOrderBasicInfo == null || id == 0)
                return BadRequest("Invalid request");
            int result = await _maintenanceOrderService.UpdateOrder(maintenanceOrderBasicInfo, id);
            return result > 0 ? Ok("Update maintenance order success") : BadRequest("Update maintenance order failed");
        }

        /// <summary>
        /// Get list of cracks in queue {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/maintenance-orders/queue
        /// </remarks>
        /// <returns>List of cracks in queue</returns>
        /// <response code="200">Returns list of cracks in queue</response>
        /// <response code="404">If not found</response>
        [HttpGet("queue")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MaintenanceWorkerInfo>>> GetQueue()
        {
            List<CrackInfo> cracks = await _maintenanceOrderService.GetQueue();
            if (cracks != null)
                return Ok(cracks);
            return NotFound();
        }

        /// <summary>
        /// Get list of maintenance orders {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/maintenance-orders
        /// </remarks>
        /// <returns>List of maintenance orders</returns>
        /// <response code="200">Returns list of maintenance orders</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MaintenanceOrderInfo>> GetMaintenanceOrders()
        {
            List<MaintenanceOrderInfo> orderInfos = _maintenanceOrderService.GetMaintenanceOrders();
            if (orderInfos != null)
                return Ok(orderInfos);
            return NotFound();
        }

        /// <summary>
        /// Get number of maintenance orders {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/maintenance-orders/count
        /// </remarks>
        /// <returns>Number of maintenance orders</returns>
        /// <response code="200">Returns list of maintenance orders</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetMaintenanceOrdersCount()
        {
            return _maintenanceOrderService.GetMaintenanceOrdersCount();
        }

        /// <summary>
        /// Get a MaintenanceOrderInfo object by <paramref name="id"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/maintenance-orders/5
        /// </remarks>
        /// <param name="id">Maintenance order Id</param>
        /// <returns>A MaintenanceOrderInfo object</returns>
        /// <response code="200">Returns a MaintenanceOrderInfo object</response>
        /// <response code="404">If no maintenance orders match this Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaintenanceOrderInfo>> GetMaintenanceOrderById(int id)
        {
            MaintenanceOrderInfo maintenanceOrder = await _maintenanceOrderService.GetById(id);
            if (maintenanceOrder == null)
                return NotFound();
            return Ok(maintenanceOrder);
        }
    }
}