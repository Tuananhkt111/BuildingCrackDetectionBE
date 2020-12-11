using CapstoneBE.Models.Custom.MaintenaceWorkers;
using CapstoneBE.Services.MaintenanceWorkers;
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
    [Route("api/v1/maintenance-workers")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MaintenanceWorkerController : ControllerBase
    {
        private readonly IMaintenanceWorkerService _maintenanceWorkerService;

        public MaintenanceWorkerController(IMaintenanceWorkerService maintenanceWorkerService)
        {
            _maintenanceWorkerService = maintenanceWorkerService;
        }

        /// <summary>
        /// Create maintenance worker {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/maintenance-workers
        /// </remarks>
        /// <param name="maintenanceWorkerBasicInfo">A MaintenanceWorkerBasicInfo object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Create maintenance worker success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Create maintenance worker failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreateMaintenanceWorker([FromBody] MaintenanceWorkerBasicInfo maintenanceWorkerBasicInfo)
        {
            if (maintenanceWorkerBasicInfo == null)
                return BadRequest("Invalid request");
            bool result = await _maintenanceWorkerService.Create(maintenanceWorkerBasicInfo);
            return result ? Ok("Create maintenance worker success") : BadRequest("Create maintenance worker failed");
        }

        /// <summary>
        /// Get list of maintenance workers {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/maintenance-workers
        /// </remarks>
        /// <returns>List of maintenance workers</returns>
        /// <response code="200">Returns list of maintenance workers</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MaintenanceWorkerInfo>> GetMaintenanceWorkers()
        {
            List<MaintenanceWorkerInfo> workerInfos = _maintenanceWorkerService.GetMaintenanceWorkers();
            if (workerInfos != null)
                return Ok(workerInfos);
            return NotFound();
        }

        /// <summary>
        /// Get number of maintenance workers {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/maintenance-workers/count
        /// </remarks>
        /// <returns>Number of maintenance workers</returns>
        /// <response code="200">Returns list of maintenance workers</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetMaintenanceWorkersCount()
        {
            return _maintenanceWorkerService.GetMaintenanceWorkersCount();
        }

        /// <summary>
        /// Delete a maintenance worker by <paramref name="id"/> {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/maintenance-workers/5
        /// </remarks>
        /// <param name="id">Maintenance worker Id</param>
        /// <returns>Message result</returns>
        /// <response code="200">If success, returns message "Delete maintenance worker success"</response>
        /// <response code="404">If failed, returns message "Maintenance worker doesn't exist"</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteMaintenanceWorker(int id)
        {
            bool delResult = await _maintenanceWorkerService.Delete(id);
            if (delResult)
                return Ok("Delete maintenance worker success");
            return NotFound("Maintenance worker doesn't exist");
        }

        /// <summary>
        /// Get a MaintenanceWorkerInfo object by <paramref name="id"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/maintenance-workers/5
        /// </remarks>
        /// <param name="id">Maintenance worker Id</param>
        /// <returns>A MaintenanceWorkerInfo object</returns>
        /// <response code="200">Returns a MaintenanceWorkerInfo object</response>
        /// <response code="404">If no maintenance workers match this Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaintenanceWorkerInfo>> GetMaintenanceWorkerById(int id)
        {
            MaintenanceWorkerInfo maintenanceWorker = await _maintenanceWorkerService.GetById(id);
            if (maintenanceWorker == null)
                return NotFound();
            return Ok(maintenanceWorker);
        }

        /// <summary>
        /// Update a maintenance worker {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/maintenance-workers/2
        /// </remarks>
        /// <param name="id">Maintenance worker Id</param>
        /// <param name="maintenanceWorkerBasicInfo">A MaintenanceWorkerBasicInfo object</param>
        /// <returns>An integer</returns>
        /// <response code="200">Returns 1</response>
        /// <response code="400">If bad request, returns message "Invalid request"</response>
        [HttpPost("{id}")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Update(int id, MaintenanceWorkerBasicInfo maintenanceWorkerBasicInfo)
        {
            if (maintenanceWorkerBasicInfo == null)
                return BadRequest("Invalid request");
            int result = await _maintenanceWorkerService.Update(maintenanceWorkerBasicInfo, id);
            return Ok(result);
        }
    }
}