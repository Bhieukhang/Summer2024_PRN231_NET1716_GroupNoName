using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        #region Login
        /// <summary>
        /// Login JWT
        /// </summary>
        /// <returns>Home</returns> 
        /// GET : api/Login
        #endregion
        [AllowAnonymous]
        [HttpPost(ApiEndPointConstant.Login.LoginEndpoint)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest == null)
                    return BadRequest("Invalid client request");

                var user = await _authService.GetAccountByPhone(loginRequest.Phone, loginRequest.Password);
                var token = await _authService.LoginAsync(loginRequest.Phone, loginRequest.Password);

                return Ok(new LoginResponse(user.Phone, user.Role.RoleName, token));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        #region Logout
        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>Login</returns> 
        /// GET : api/Logout
        #endregion
        [HttpPost(ApiEndPointConstant.Logout.LogoutEndpoint)]
        public IActionResult Logout()
        {

            return Ok(new { message = "Logout successful" });
        }

    }
}