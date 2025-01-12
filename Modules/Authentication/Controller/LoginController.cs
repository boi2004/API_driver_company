using Driver_Company_5._0.Modules.Authentication.Models;
using Driver_Company_5._0.Modules.Authentication.Services;
using Driver_Company_5._0.Modules.Authentication.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Driver_Company_5._0.Modules.Authentication.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        /// <summary>
        /// Khởi tạo LoginController với việc Inject LoginService.
        /// </summary>
        /// <param name="loginService">Dịch vụ xử lý logic đăng nhập.</param>
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        //Hàm định dạng mật khẩu


        /// <summary>
        /// Xử lý yêu cầu đăng nhập từ người dùng.
        /// </summary>
        /// <param name="request">Thông tin đăng nhập bao gồm Username và Password.</param>
        /// <returns>Trả về kết quả xác thực và token nếu thành công.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Kiểm tra nếu request null
            if (request == null)
            {
                return BadRequest(new
                {
                    Message = "Dữ liệu yêu cầu không được để trống.",
                    StatusCode = 400
                });
            }

            // Kiểm tra dữ liệu đầu vào có hợp lệ không
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Dữ liệu không hợp lệ.",
                    Errors = errorMessages,
                    StatusCode = 400
                });
            }

            // Gọi LoginService để xử lý logic đăng nhập
            var authResponse = await _loginService.LoginUser(request);

            // Nếu đăng nhập thất bại, trả về lỗi cụ thể
            if (!authResponse.Success)
            {
                return Unauthorized(new
                {
                    Message = authResponse.Message,
                    StatusCode = 401
                });
            }

            // Nếu đăng nhập thành công, trả về token và thông báo thành công
            return Ok(new
            {
                Message = "Đăng nhập thành công!",
                Token = authResponse.Token,
                StatusCode = 200
            });
        }
    }
}