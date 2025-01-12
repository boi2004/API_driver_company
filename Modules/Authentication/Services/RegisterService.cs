// File: RegisterService.cs
using Driver_Company_5._0.Infrastructure.Data;
using Driver_Company_5._0.Models;
using Driver_Company_5._0.Modules.Authentication.Models;
using Driver_Company_5._0.Modules.Authentication.Security;
using Microsoft.EntityFrameworkCore;

namespace Driver_Company_5._0.Modules.Authentication.Services
{
    public class RegisterService
    {
        private readonly DriverManagementContext _context;

        /// <summary>
        /// Constructor với Dependency Injection cho DbContext
        /// </summary>
        public RegisterService(DriverManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Xử lý đăng ký người dùng mới
        /// </summary>
        public async Task<AuthResponse> RegisterUser(RegisterRequest request)
        {
            // Kiểm tra Username hoặc Email đã tồn tại chưa
            if (await _context.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Username hoặc Email đã tồn tại trong hệ thống."
                };
            }

            // Kiểm tra định dạng và xác nhận mật khẩu
            if (request.Password != request.ConfirmPassword)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Mật khẩu và xác nhận mật khẩu không khớp."
                };
            }

            // Băm mật khẩu với BCrypt
            var hashedPassword = PasswordHasher.HashPassword(request.Password);

            // Tạo đối tượng User mới
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = hashedPassword,
                Email = request.Email,
                Phone = request.PhoneNumber,
                Address = request.Address,
                City = request.City,
                Birthday = request.Birthday
            };

            // Thêm vào CSDL và lưu
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // Trả về kết quả thành công
            return new AuthResponse
            {
                Success = true,
                Message = "Đăng ký tài khoản thành công!"
            };
        }
    }
}
