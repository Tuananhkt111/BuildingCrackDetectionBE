using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Users;
using CapstoneBE.Services.Emails;
using CapstoneBE.Services.PushNotifications;
using CapstoneBE.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Controllers
{
    [Authorize]
    [Route("api/v1/users")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly INotificationService _notificationService;

        public UserController(IUserService userService, IEmailService emailService, INotificationService notificationService)
        {
            _userService = userService;
            _emailService = emailService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Authenticate to system by userName and password (Staff role)
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/users/authenticate/staff
        /// </remarks>
        /// <param name="userLogin">A UserLogin object</param>
        /// <returns>A UserLoginResponse object</returns>
        /// <response code="200">Returns the UserLoginResponse object</response>
        /// <response code="400">If bad request, returns message "Invalid request"</response>
        /// <response code="404">If sign-in failed, returns message "Sign-in Failed"</response>
        [AllowAnonymous]
        [HttpPost("authenticate/staff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserLoginResponse>> AuthenticateStaff([FromBody] UserLogin userLogin)
        {
            if (userLogin == null)
                return BadRequest("Invalid request");
            UserLoginResponse jwtToken = await _userService.Authenticate(userLogin.UserName, userLogin.Password, userLogin.FcmToken, false);
            if (jwtToken == null)
                return NotFound("Sign-in Failed");
            return Ok(jwtToken);
        }

        /// <summary>
        /// Authenticate to system by userName and password (Administrator, Manager Role)
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/users/authenticate/manager
        /// </remarks>
        /// <param name="userLogin">A UserLogin object</param>
        /// <returns>A UserLoginResponse object</returns>
        /// <response code="200">Returns the UserLoginResponse object</response>
        /// <response code="400">If bad request, returns message "Invalid request"</response>
        /// <response code="404">If sign-in failed, returns message "Sign-in Failed"</response>
        [AllowAnonymous]
        [HttpPost("authenticate/manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserLoginResponse>> Authenticate([FromBody] UserLogin userLogin)
        {
            if (userLogin == null)
                return BadRequest("Invalid request");
            UserLoginResponse jwtToken = await _userService.Authenticate(userLogin.UserName, userLogin.Password, userLogin.FcmToken, true);
            if (jwtToken == null)
                return NotFound("Sign-in Failed");
            return Ok(jwtToken);
        }

        /// <summary>
        /// Get a UserInfo object by <paramref name="id"/> {Auth Roles: Administrator, Manager, Staffs}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/users/5
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <returns>A UserInfo object</returns>
        /// <response code="200">Returns a UserInfo object</response>
        /// <response code="404">If no users match this Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfo>> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Delete a user by <paramref name="id"/> {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: DELETE: api/v1/users/5
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <returns>Message result</returns>
        /// <response code="200">If success, returns message "Delete user success"</response>
        /// <response code="404">If failed, returns message "User doesn't exist"</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteUser(string id)
        {
            bool delResult = await _userService.DeleteUser(id);
            if (delResult)
                return Ok("Delete user success");
            return NotFound("User doesn't exist");
        }

        /// <summary>
        /// Update a user in some basic information {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// <para>Sample request: POST: api/v1/users/1/basic/admin</para>
        /// <para>Administrator role: Can update phone, email, name, address</para>
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <param name="userBasicInfo">A UserBasicInfo object</param>
        /// <returns>An integer</returns>
        /// <response code="200">Returns 1</response>
        /// <response code="400">
        /// <para>If bad request, returns message "Invalid request"</para>
        /// <para>If user role is admin or not existed, returns message "Role value is forbidden"</para>
        /// <para>If user role is manager, returns message "Manager belongs to some locations"</para>
        /// <para>If user role is staff, returns message "Staff belongs only to one location"</para>
        /// </response>
        [HttpPost("{id}")]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> UpdateBasicInfo(string id, UserBasicInfo userBasicInfo)
        {
            if (userBasicInfo == null)
                return BadRequest("Invalid request");
            UserInfo user = await _userService.GetUserById(id);
            int[] locationIds = userBasicInfo.LocationIds;
            switch (user.Role)
            {
                case Roles.ManagerRole:
                    if (locationIds == null || locationIds.Length <= 0)
                        return BadRequest("Manager belongs to some locations");
                    break;

                case Roles.StaffRole:
                    if (locationIds == null || locationIds.Length != 1)
                        return BadRequest("Staff belongs only to one location");
                    break;

                default: return BadRequest("Role value is forbidden");
            }
            int result = await _userService.UpdateBasicInfo(userBasicInfo, id);
            if (result > 0 && (user.Role.Equals(Roles.ManagerRole) || user.Role.Equals(Roles.StaffRole)))
            {
                string[] receiverIds = { id };
                _ = _notificationService.SendNotifications(null, receiverIds, MessageType.AdminUpdateInfo);
            }
            return Ok(result);
        }

        /// <summary>
        /// Change password of account {Auth Roles: Administrator, Manager, Staff}
        /// </summary>
        /// <remarks>
        /// <para>Sample request: POST: api/v1/users/3/password</para>
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <param name="userPass">A UserPass object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Change password success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Change password failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// </response>
        [HttpPost("{id}/password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ChangePassword(string id, UserPass userPass)
        {
            if (userPass == null)
                return BadRequest("Invalid request");
            bool result = await _userService.ChangePassword(userPass.OldPass, userPass.NewPass, id);
            return result ? Ok("Change password success") : BadRequest("Change password failed");
        }

        /// <summary>
        /// Reset password of account {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// <para>Sample request: POST: api/v1/users/3/resetpass</para>
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Reset password success"</response>
        /// <response code="400">If failed, returns message "Reset password failed"</response>
        [HttpPost("{id}/resetpass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ResetPassword(string id)
        {
            Email email = await _userService.ResetPassword(id);
            if (email != null)
            {
                _ = _emailService.SendEmailAsync(email);
                return Ok("Reset password success");
            }
            return BadRequest("Reset password failed");
        }

        /// <summary>
        /// Forgot password of account confirm
        /// </summary>
        /// <remarks>
        /// <para>Sample request: POST: api/v1/users/3/forgotpass-confirm</para>
        /// </remarks>
        /// <param name="userName">UserName</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Reset password URL has been sent to the email successfully!"</response>
        /// <response code="400">If failed, returns message "Reset password URL has been sent to the email failed!"</response>
        [HttpPost("forgotpass-confirm")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ForgotPasswordConfirm(string userName)
        {
            string rootPath = this.Request.Scheme + "://" + this.Request.Host.Value + this.Request.Path.Value;
            rootPath = rootPath.Replace("forgotpass-confirm", "");
            Email email = await _userService.ForgotPassword(userName, rootPath);
            if (email != null)
            {
                _ = _emailService.SendEmailAsync(email);
                return Ok("Reset password URL has been sent to the email successfully!");
            }
            return BadRequest("Reset password URL has been sent to the email failed!");
        }

        /// <summary>
        /// Forgot password of account
        /// </summary>
        /// <remarks>
        /// <para>Sample request: POST: api/v1/users/3/forgotpass</para>
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <param name="token">Reset password token</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Reset password success"</response>
        /// <response code="400">If failed, returns message "Reset password failed"</response>
        [HttpGet("{id}/forgotpass")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ForgotPassword(string id, string token)
        {
            Email email = await _userService.ResetPassword(id, token);
            if (email != null)
            {
                _ = _emailService.SendEmailAsync(email);
                return Ok("Reset password success");
            }
            return BadRequest("Reset password failed");
        }

        /// <summary>
        /// Create account {Auth Roles: Administrator}
        /// </summary>
        /// <remarks>
        /// Sample request: POST: api/v1/users
        /// </remarks>
        /// <param name="user">A UserCreate object</param>
        /// <returns>Result message</returns>
        /// <response code="200">If success, returns message "Create user success"</response>
        /// <response code="400">
        /// <para>If failed, returns message "Create user failed"</para>
        /// <para>If bad request, returns message "Invalid request"</para>
        /// <para>If user role is admin or not existed, returns message "Role value is forbidden"</para>
        /// <para>If user role is manager, returns message "Manager belongs to some locations"</para>
        /// <para>If user role is staff, returns message "Staff belongs only to one location"</para>
        /// </response>
        [HttpPost]
        [Authorize(Roles = Roles.AdminRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreateUser([FromBody] UserCreate user)
        {
            if (user == null)
                return BadRequest("Invalid request");
            int[] locationIds = user.LocationIds;
            switch (user.Role)
            {
                case Roles.ManagerRole:
                    if (locationIds == null || locationIds.Length <= 0)
                        return BadRequest("Manager belongs to some locations");
                    break;

                case Roles.StaffRole:
                    if (locationIds == null || locationIds.Length != 1)
                        return BadRequest("Staff belongs only to one location");
                    break;

                default: return BadRequest("Role value is forbidden");
            }
            Email email = await _userService.CreateUser(user);
            if (email == null)
                return BadRequest("Create user failed");
            _ = _emailService.SendEmailAsync(email);
            return Ok("Create user success");
        }

        /// <summary>
        /// Get list of users {Auth Roles: Administrator, Manager}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/users
        /// </remarks>
        /// <returns>List of users</returns>
        /// <response code="200">Returns list of users</response>
        /// <response code="404">If not found</response>
        [HttpGet]
        [Authorize(Roles = Roles.AdminRole + ", " + Roles.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserInfo>> GetUsers()
        {
            List<UserInfo> userInfos = _userService.GetUsers();
            if (userInfos != null)
                return Ok(userInfos);
            return NotFound();
        }

        /// <summary>
        /// Get number of users {Auth Roles: Administrator, Manager}
        /// </summary>
        /// <remarks>
        /// Sample request: GET: api/v1/users/count
        /// </remarks>
        /// <returns>Number of users</returns>
        /// <response code="200">Returns list of users</response>
        [HttpGet("count")]
        [Authorize(Roles = Roles.AdminRole + ", " + Roles.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetUsersCount()
        {
            return _userService.GetUsersCount();
        }
    }
}