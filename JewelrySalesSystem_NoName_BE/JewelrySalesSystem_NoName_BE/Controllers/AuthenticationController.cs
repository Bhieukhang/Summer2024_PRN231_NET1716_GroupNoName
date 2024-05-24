using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
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
        [HttpPost(ApiEndPointConstant.Login.LoginEndpoint)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await _authService.GetAccountByPhone(loginRequest.Phone, loginRequest.Password);
                var token = await _authService.LoginAsync(loginRequest.Phone, loginRequest.Password);

                return Ok(new LoginResponse(user.Phone, user.RoleId.ToString(), token));
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
            // Thực hiện các thao tác cần thiết để logout người dùng
            // Ví dụ: Xóa token khỏi cơ sở dữ liệu hoặc danh sách token bị vô hiệu hóa

            return Ok(new { message = "Logout successful" });
        }

    }
}