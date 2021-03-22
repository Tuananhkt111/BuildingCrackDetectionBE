using CapstoneBE.Models.Custom.Locations;
using CapstoneBE.Services.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Controllers
{
    [Authorize]
    [Route("api/v1/locations")]
    [ApiVersion("1.0")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Create location {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/locations
        /// </remarks>
        /// <param name="locationBasicInfo">A LocationBasicInfo object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Create location success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Create location failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreateLocation([FromBody] LocationBasicInfo locationBasicInfo)
        {
            if (locationBasicInfo == null)
                return BadRequest("Invalid request");
            bool result = await _locationService.Create(locationBasicInfo);
            return result ? Ok("Create location success") : BadRequest("Create location failed");
        }

        /// <summary>
        /// Get list of locations {Auth Roles: Administrator, Manager}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/locations
        /// </remarks>
        /// <returns>List of locations</returns>
        /// <response code="200">Returns list of locations</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [Authorize(Roles = Roles.AdminRole + ", " + Roles.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LocationInfo>> GetLocations()
        {
            List<LocationInfo> locationInfos = _locationService.GetLocations();
            if (locationInfos != null)
                return Ok(locationInfos);
            return NotFound();
        }

        /// <summary>
        /// Get list of available locations {Auth Roles: Administrator, Manager}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/locations/available?role=Staff
        /// </remarks>
        /// <param name="role">Employee role</param>
        /// <param name="empId">Employee Id</param>
        /// <returns>List of locations</returns>
        /// <response code="200">Returns list of locations</response>
        /// <response code="404">If not found</response>
        [HttpGet("available")]
        [Authorize(Roles = Roles.AdminRole + ", " + Roles.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LocationInfo>> GetAvailableLocations(string role, string empId = null)
        {
            if (string.IsNullOrEmpty(role))
                return NotFound();
            List<LocationInfo> locationInfos = _locationService.GetAvailableLocations(role, empId);
            if (locationInfos != null)
                return Ok(locationInfos);
            return NotFound();
        }

        /// <summary>
        /// Get number of locations {Auth Roles: Administrator, Manager}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/locations/count
        /// </remarks>
        /// <returns>Number of locations</returns>
        /// <response code="200">Returns list of locations</response>
        [HttpGet("count")]
        [Authorize(Roles = Roles.AdminRole + ", " + Roles.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetLocationsCount()
        {
            return _locationService.GetLocationsCount();
        }

        /// <summary>
        /// Delete a location by <paramref name="id"/> {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/locations/5
        /// </remarks>
        /// <param name="id">Location Id</param>
        /// <returns>Message result</returns>
        /// <response code="200">If success, returns message "Delete location success"</response>
        /// <response code="404">If failed, returns message "Delete location success"</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteLocation(int id)
        {
            bool delResult = await _locationService.Delete(id);
            if (delResult)
                return Ok("Delete location success");
            return NotFound("Delete location failed");
        }

        /// <summary>
        /// Get a LocationInfo object by <paramref name="id"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/locations/5
        /// </remarks>
        /// <param name="id">Location Id</param>
        /// <returns>A LocationInfo object</returns>
        /// <response code="200">Returns a LocationInfo object</response>
        /// <response code="404">If no locations match this Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LocationInfo>> GetLocationById(int id)
        {
            LocationInfo location = await _locationService.GetById(id);
            if (location == null)
                return NotFound();
            return Ok(location);
        }

        /// <summary>
        /// Update a location {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/locations/2
        /// </remarks>
        /// <param name="id">Location Id</param>
        /// <param name="locationBasicInfo">A LocationBasicInfo object</param>
        /// <returns>An integer</returns>
        /// <response code="200">Returns 1</response>
        /// <response code="400">If bad request, returns message "Invalid request"</response>
        [HttpPost("{id}")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Update(int id, LocationBasicInfo locationBasicInfo)
        {
            if (locationBasicInfo == null)
                return BadRequest("Invalid request");
            int result = await _locationService.Update(locationBasicInfo, id);
            return Ok(result);
        }
    }
}