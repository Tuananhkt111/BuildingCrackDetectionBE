using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.Services.Cracks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Controllers
{
    [Authorize]
    [Route("api/v1/cracks")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CrackController : ControllerBase
    {
        private readonly ICrackService _crackService;

        public CrackController(ICrackService crackService)
        {
            _crackService = crackService;
        }

        /// <summary>
        /// Create cracks {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/cracks
        /// </remarks>
        /// <param name="crackCreates">An array of CrackCreate objects</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Create cracks success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Create cracks failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost("multi")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreateCracks([FromBody] CrackCreate[] crackCreates)
        {
            if (crackCreates == null || crackCreates.Length <= 0)
                return BadRequest("Invalid request");
            bool result = await _crackService.CreateRange(crackCreates);
            return result ? Ok("Create cracks success") : BadRequest("Create cracks failed");
        }

        /// <summary>
        /// Create crack {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/crack
        /// </remarks>
        /// <param name="crackCreate">A CrackCreate objects</param>
        /// <returns>Crack Id</returns>
        /// <response code="200">If success, returns crack id</response>
        /// <response code="400">
        /// <para>If failed, returns bad request</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreateCrack([FromBody] CrackCreate crackCreate)
        {
            if (crackCreate == null)
                return BadRequest("Invalid request");
            int result = await _crackService.Create(crackCreate);
            return result > 0 ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Delete a crack by <paramref name="id"/> {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/cracks/5
        /// </remarks>
        /// <param name="id">Crack Id</param>
        /// <returns>Message result</returns>
        /// <response code="200">If success, returns message "Delete crack success"</response>
        /// <response code="404">If failed, returns message "Crack doesn't exist"</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteCrack(int id)
        {
            bool delResult = await _crackService.Delete(id);
            if (delResult)
                return Ok("Delete crack success");
            return NotFound("Crack doesn't exist");
        }

        /// <summary>
        /// Update a crack or verify crack {Auth Roles: Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/cracks/2
        /// </remarks>
        /// <param name="id">Location Id</param>
        /// <param name="crackBasicInfo">A CrackBasicInfo object</param>
        /// <returns>An integer</returns>
        /// <response code="200">Returns message "Update crack success"</response>
        /// <response code="400">
        /// <para>If bad request, returns message "Invalid request"</para>
        /// <para>If update failed, returns message "Update crack failed"</para>
        /// </response>
        [HttpPost("{id}")]
        [Authorize(Roles = Roles.StaffRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Update(int id, CrackBasicInfo crackBasicInfo)
        {
            if (crackBasicInfo == null)
                return BadRequest("Invalid request");
            bool result = await _crackService.Update(crackBasicInfo, id);
            return result ? Ok("Update crack success") : BadRequest("Update crack failed");
        }

        /// <summary>
        /// Get list of cracks {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/cracks
        /// </remarks>
        /// <param name="status">Crack status</param>
        /// <param name="ignore">Crack status ignored</param>
        /// <returns>List of cracks</returns>
        /// <response code="200">Returns list of cracks</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CrackInfo>> GetCracks(string status = "", string ignore = "")
        {
            List<CrackInfo> crackInfos = null;
            if (string.IsNullOrEmpty(status) && string.IsNullOrEmpty(ignore))
                crackInfos = _crackService.GetCracks();
            else if (!string.IsNullOrEmpty(status) && string.IsNullOrEmpty(ignore))
                crackInfos = _crackService.GetCracks(status);
            else if (string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(ignore))
                crackInfos = _crackService.GetCracksIgnore(ignore);
            if (crackInfos != null)
                return Ok(crackInfos);
            return NotFound();
        }

        /// <summary>
        /// Get number of cracks {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/cracks/count
        /// </remarks>
        /// <returns>Number of cracks</returns>
        /// <response code="200">Returns list of cracks</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetCracksCount()
        {
            return _crackService.GetCracksCount();
        }

        /// <summary>
        /// Get a CrackInfo object by <paramref name="id"/> {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/cracks/5
        /// </remarks>
        /// <param name="id">Crack Id</param>
        /// <returns>A CrackInfo object</returns>
        /// <response code="200">Returns a CrackInfo object</response>
        /// <response code="404">If no cracks match this Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CrackInfo>> GetCrackById(int id)
        {
            CrackInfo crack = await _crackService.GetById(id);
            if (crack == null)
                return NotFound();
            return Ok(crack);
        }
    }
}