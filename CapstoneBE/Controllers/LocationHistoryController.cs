using CapstoneBE.Models.Custom.LocationHistories;
using CapstoneBE.Services.LocationHistories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Controllers
{
    [Authorize]
    [Route("api/v1/location-histories")]
    [ApiVersion("1.0")]
    [ApiController]
    public class LocationHistoryController : ControllerBase
    {
        private readonly ILocationHistoryService _locationHistoryService;

        public LocationHistoryController(ILocationHistoryService locationHistoryService)
        {
            _locationHistoryService = locationHistoryService;
        }

        /// <summary>
        /// Create location history {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/location-histories
        /// </remarks>
        /// <param name="locationHistoryCreate">A LocationHistoryCreate object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Create location history success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Create location history failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreateLocationHistory([FromBody] LocationHistoryCreate locationHistoryCreate)
        {
            if (locationHistoryCreate == null)
                return BadRequest("Invalid request");
            bool result = await _locationHistoryService.Create(locationHistoryCreate);
            return result ? Ok("Create location history success") : BadRequest("Create location history failed");
        }

        /// <summary>
        /// Update location history {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/location-histories/1
        /// </remarks>
        /// <param name="id">Location History Id</param>
        /// <param name="locationHistoryUpdate">A LocationHistoryUpdate object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Update location history success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Update location history failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost("{id}")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateLocationHistory(int id, [FromBody] LocationHistoryUpdate locationHistoryUpdate)
        {
            if (locationHistoryUpdate == null)
                return BadRequest("Invalid request");
            bool result = await _locationHistoryService.Update(locationHistoryUpdate, id);
            return result ? Ok("Update location history success") : BadRequest("Update location history failed");
        }

        /// <summary>
        /// Get list of location histories by <paramref name="userId"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/location-histories
        /// </remarks>
        /// <param name="userId">User Id</param>
        /// <returns>List of location histories</returns>
        /// <response code="200">Returns list of location histories</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LocationHistoryInfo>> GetLocationHistories(string userId)
        {
            List<LocationHistoryInfo> locationHistoryInfos = _locationHistoryService.GetLocationHistories(userId);
            if (locationHistoryInfos != null)
                return Ok(locationHistoryInfos);
            return NotFound();
        }

        /// <summary>
        /// Get number of location histories by <paramref name="userId"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/location-histories/count
        /// </remarks>
        /// <param name="userId">User Id</param>
        /// <returns>Number of location histories</returns>
        /// <response code="200">Returns list of location histories</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetLocationHistoriesCount(string userId)
        {
            return _locationHistoryService.GetLocationHistoriesCount(userId);
        }

        /// <summary>
        /// Delete a location history by <paramref name="id"/> {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/location-histories/5
        /// </remarks>
        /// <param name="id">Location History Id</param>
        /// <returns>Message result</returns>
        /// <response code="200">If success, returns message "Delete location history success"</response>
        /// <response code="404">If failed, returns message "Location history doesn't exist"</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteLocationHistory(int id)
        {
            bool delResult = await _locationHistoryService.Delete(id);
            if (delResult)
                return Ok("Delete location history success");
            return NotFound("Location history doesn't exist");
        }

        /// <summary>
        /// Get a LocationHistoryInfo object by <paramref name="id"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/location-histories/5
        /// </remarks>
        /// <param name="id">Location History Id</param>
        /// <returns>A LocationHistoryInfo object</returns>
        /// <response code="200">Returns a LocationHistoryInfo object</response>
        /// <response code="404">If no location histories match this Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LocationHistoryInfo>> GetLocationHistoryById(int id)
        {
            LocationHistoryInfo locationHistory = await _locationHistoryService.GetById(id);
            if (locationHistory == null)
                return NotFound();
            return Ok(locationHistory);
        }
    }
}