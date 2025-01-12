using Driver_Company_5._0.Infrastructure.Data;               // Import DbContext
using Driver_Company_5._0.Models;                           // Import Model User
using Driver_Company_5._0.Modules.Authentication.Models;    // Import các model xác thực
using Driver_Company_5._0.Modules.Authentication.Security;  // Import JwtHelper
using Microsoft.EntityFrameworkCore;                        // Import Entity Framework Core

namespace Driver_Company_5._0.Modules.Authentication.Services
{
    /// Service chuyên xử lý logic đăng nhập người dùng
    public class LoginService
    {
        private readonly DriverManagementContext _context; // Đối tượng DbContext để tương tác với CSDL
        private readonly JwtHelper _jwtHelper;             // Đối tượng JwtHelper để tạo JWT Token

        //  Constructor - Inject DbContext và JwtHelper
        public LoginService(DriverManagementContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// Phương thức đăng nhập người dùng
        /// </summary>
        public async Task<AuthResponse> LoginUser(LoginRequest request)
        {
            // Bước 1: Tìm kiếm người dùng trong cơ sở dữ liệu theo Username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            // Bước 2: Kiểm tra nếu không tìm thấy hoặc sai mật khẩu
            if (user == null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản!"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Mật khẩu không chính xác!"
                };
            }

            // Bước 3: Tạo JWT Token với thời gian sống tùy chỉnh
            var tokenLifetime = request.RememberMe ? TimeSpan.FromDays(30) : TimeSpan.FromHours(2); // 30 ngày nếu ghi nhớ, 2 giờ nếu không
            try
            {
                var token = _jwtHelper.GenerateJwtToken(user, tokenLifetime);

                // Bước 4: Trả về phản hồi thành công với Token
                return new AuthResponse
                {
                    Success = true,
                    Message = "Đăng nhập thành công!",
                    Token = token
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Lỗi trong quá trình tạo token: {ex.Message}"
                };
            }
        }

    }
}
