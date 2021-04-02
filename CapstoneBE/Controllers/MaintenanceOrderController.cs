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
using CapstoneBE.Utils;
using static CapstoneBE.Utils.APIConstants;
using CapstoneBE.Models;

namespace CapstoneBE.Controllers
{
    [Authorize]
    [Route("api/v1/maintenance-orders")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MaintenanceOrderController : ControllerBase
    {
        private readonly IMaintenanceOrderService _maintenanceOrderService;

        public MaintenanceOrderController(IMaintenanceOrderService maintenanceOrderService)
        {
            _maintenanceOrderService = maintenanceOrderService;
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
        public async Task<ActionResult<IEnumerable<CrackSubDetailsInfo>>> GetQueue()
        {
            List<CrackSubDetailsInfo> cracks = await _maintenanceOrderService.GetQueue();
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
        /// <param name="status">Maintenance order status</param>
        /// <returns>List of maintenance orders</returns>
        /// <response code="200">Returns list of maintenance orders</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MaintenanceOrderInfo>> GetMaintenanceOrders(string status)
        {
            List<MaintenanceOrderInfo> orderInfos;
            if (!string.IsNullOrEmpty(status))
                orderInfos = _maintenanceOrderService.GetMaintenanceOrders(status);
            else
                orderInfos = _maintenanceOrderService.GetMaintenanceOrders();
            if (orderInfos != null)
                return Ok(orderInfos);
            return NotFound();
        }

        /// <summary>
        /// Get number of maintenance orders {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// <param name="locationId">Location Id</param>
        /// <param name="year">Year of Checkup</param>
        /// Sample request: GET: api/v1/maintenance-orders/count
        /// </remarks>
        /// <returns>Number of maintenance orders</returns>
        /// <response code="200">Returns list of maintenance orders</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetMaintenanceOrdersCount(int year, int locationId)
        {
            if (year <= 0 || locationId <= 0)
                return BadRequest();
            int result = _maintenanceOrderService.GetMaintenanceOrdersCount(year, locationId);
            if (result > 0)
                return Ok(result);
            else
                return NotFound();
        }

        /// <summary>
        /// Get total expense of maintenance orders {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// <param name="locationId">Location Id</param>
        /// <param name="year">Year of Checkup</param>
        /// Sample request: GET: api/v1/maintenance-orders/count/expense-total
        /// </remarks>
        /// <returns>Number of maintenance orders</returns>
        /// <response code="200">Returns list of maintenance orders</response>
        [HttpGet("count/expense-total")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<float> GetMaintenanceOrdersExpenseTotal(int year, int locationId)
        {
            if (year <= 0 || locationId <= 0)
                return BadRequest();
            float result = _maintenanceOrderService.GetMaintenanceOrdersExpenseTotal(year, locationId);
            if (result > 0)
                return Ok(result);
            else
                return NotFound();
        }

        /// <summary>
        /// Get average assessment of maintenance orders {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// <param name="locationId">Location Id</param>
        /// <param name="year">Year of Checkup</param>
        /// Sample request: GET: api/v1/maintenance-orders/count/assessment-average
        /// </remarks>
        /// <returns>Number of maintenance orders</returns>
        /// <response code="200">Returns list of maintenance orders</response>
        [HttpGet("count/assessment-average")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<float> GetMaintenanceOrdersAssessmentAverage(int year, int locationId)
        {
            if (year <= 0 || locationId <= 0)
                return BadRequest();
            float result = _maintenanceOrderService.GetMaintenanceOrdersExpenseTotal(year, locationId);
            if (result > 0)
                return Ok(result);
            else
                return NotFound();
        }

        /// <summary>
        /// Get number of maintenance orders count by status {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/maintenance-orders/count/status
        /// </remarks>
        /// <param name="period">Period of Checkup</param>
        /// <param name="locationIdsStr">Location Ids</param>
        /// <param name="year">Year of Checkup</param>
        /// <returns>Number of maintenance orders</returns>
        /// <response code="200">Returns list of maintenance orders</response>
        [HttpGet("count/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ChartValue>> GetMaintenanceOrdersCountByStatus(int period, int year, string locationIdsStr)
        {
            int[] locationIds = MyUtils.ConvertStringToIntArray(locationIdsStr);
            if (period > 3 || period < 1 || year <= 0)
                return BadRequest();
            List<ChartValue> result = _maintenanceOrderService.GetMaintenanceOrdersCountByStatus(period, year, locationIds);
            if (result != null && result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }

        /// <summary>
        /// Get number of maintenance orders expenses {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/maintenance-orders/count/expense
        /// </remarks>
        /// <param name="locationId">Location Id</param>
        /// <param name="year">Year of Checkup</param>
        /// <returns>Number of maintenance orders</returns>
        /// <response code="200">Returns list of maintenance orders</response>
        [HttpGet("count/expense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ChartValueFloat>> GetMaintenanceOrdersExpense(int year, int locationId)
        {
            if (locationId <= 0 || year <= 0)
                return BadRequest();
            List<ChartValueFloat> result = _maintenanceOrderService.GetMaintenanceOrdersExpense(year, locationId);
            if (result != null && result.Count > 0)
                return Ok(result);
            else
                return NotFound();
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