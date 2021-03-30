using CapstoneBE.Models.Custom.Flights;
using CapstoneBE.Services.Flights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Controllers
{
    [Authorize]
    [Route("api/v1/flights")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        /// <summary>
        /// Create flight {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/flights
        /// </remarks>
        /// <param name="flightCreate">A FlightCreate object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Create flight success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Create flight failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateFlight([FromBody] FlightCreate flightCreate)
        {
            FlightBasicInfo result = await _flightService.Create(flightCreate);
            return result != null ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Get list of flights {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/flights
        /// </remarks>
        /// <returns>List of flights</returns>
        /// <response code="200">Returns list of flights</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<FlightInfo>> GetFlights()
        {
            List<FlightInfo> flightInfos = _flightService.GetFlights();
            if (flightInfos != null)
                return Ok(flightInfos);
            return NotFound();
        }

        /// <summary>
        /// Get number of flights {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/flights/count
        /// </remarks>
        /// <returns>Number of flights</returns>
        /// <response code="200">Returns list of flights</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetFlightsCount()
        {
            return _flightService.GetFlightsCount();
        }

        /// <summary>
        /// Get a FlightInfo object by <paramref name="id"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/flights/5
        /// </remarks>
        /// <param name="id">Flight Id</param>
        /// <returns>A FlightInfo object</returns>
        /// <response code="200">Returns a FlightInfo object</response>
        /// <response code="404">If no flights match this Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FlightInfo>> GetFlightById(int id)
        {
            FlightInfo flight = await _flightService.GetById(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }

        /// <summary>
        /// Remove video of object <paramref name="id"/> {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/flights/5/video
        /// </remarks>
        /// <param name="id">Flight Id</param>
        /// <returns>Result message</returns>
        /// <response code="200">Returns message "Remove video success"</response>
        /// <response code="400">Returns message "Remove video failed"</response>
        [HttpDelete("{id}/video")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> RemoveVideo(int id)
        {
            bool result = await _flightService.RemoveVideo(id);
            return result ? Ok("Remove video success") : BadRequest("Remove video failed");
        }

        /// <summary>
        /// Check video exists or not {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/flights/exists-db
        /// </remarks>
        /// <param name="video">Video</param>
        /// <returns>Result message</returns>
        /// <response code="200">Returns message "Video exists"</response>
        /// <response code="404">Returns message "Video not found"</response>
        [HttpGet("exists-db")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> CheckVideoExistsInDatabase(string video)
        {
            bool result = _flightService.CheckExistsInDatabase(video);
            return result ? Ok("Video exists") : NotFound("Video not found");
        }

        /// <summary>
        /// Check video exists or not {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/flights/exists
        /// </remarks>
        /// <param name="video">Video</param>
        /// <returns>Result message</returns>
        /// <response code="200">Returns message "Video exists"</response>
        /// <response code="404">Returns message "Video not found"</response>
        [HttpGet("exists")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> CheckVideoExistsInStorage(string video)
        {
            bool result = _flightService.CheckExistsInStorage(video);
            return result ? Ok("Video exists") : NotFound("Video not found");
        }
    }
}