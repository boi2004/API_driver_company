using System; // Thư viện cơ bản chứa các kiểu dữ liệu và chức năng chung
using System.Collections.Generic; // Hỗ trợ các cấu trúc dữ liệu như List, Dictionary
using System.Linq; // Cung cấp các phương thức LINQ để xử lý dữ liệu
using System.Threading.Tasks; // Hỗ trợ lập trình bất đồng bộ với async/await
using Microsoft.AspNetCore.Http; // Làm việc với HTTP (yêu cầu, phản hồi)
using Microsoft.AspNetCore.Mvc; // Xây dựng API và xử lý yêu cầu HTTP
using Microsoft.EntityFrameworkCore; // ORM để làm việc với cơ sở dữ liệu
using Driver_Company_5._0.Models; // Namespace chứa các lớp mô hình scaffold từ cơ sở dữ liệu
using BCrypt.Net;
using Driver_Company_5._0.Infrastructure.Data;


namespace Driver_Company_5._0.Controllers // Định nghĩa không gian tên của controller
{
    [Route("api/[controller]")] // Định nghĩa route API, tự động gắn tên controller (UsersController -> /api/Users)
    [ApiController] // Xác định đây là một API Controller, hỗ trợ xử lý HTTP và validation tự động
    public class UsersController : ControllerBase // Kế thừa ControllerBase (dành cho API, không có View)
    {
        private readonly DriverManagementContext _context; // Biến để truy cập cơ sở dữ liệu thông qua DbContext

        // Constructor để inject DriverManagementContext thông qua Dependency Injection
        public UsersController(DriverManagementContext context)
        {
            _context = context; // Gán DbContext cho biến nội bộ
        }

        // GET: api/Users - Lấy danh sách tất cả người dùng
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync(); // Lấy danh sách người dùng từ bảng Users
        }

        // GET: api/Users/5 - Lấy thông tin người dùng theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id); // Tìm người dùng theo ID

            if (user == null) // Nếu không tìm thấy, trả về 404 NotFound
            {
                return NotFound();
            }

            return user; // Trả về thông tin người dùng
        }

        // PUT: api/Users/5 - Cập nhật thông tin người dùng
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id) // Kiểm tra xem ID trong route có khớp với ID trong dữ liệu không
            {
                return BadRequest(); // Nếu không khớp, trả về 400 BadRequest
            }

            _context.Entry(user).State = EntityState.Modified; // Đánh dấu thực thể User là "đã sửa đổi"

            try
            {
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            catch (DbUpdateConcurrencyException) // Xử lý lỗi xung đột dữ liệu khi cập nhật
            {
                if (!UserExists(id)) // Kiểm tra xem người dùng có tồn tại không
                {
                    return NotFound(); // Nếu không, trả về 404 NotFound
                }
                else
                {
                    throw; // Nếu là lỗi khác, ném lại lỗi
                }
            }

            return NoContent(); // Trả về 204 NoContent (không có nội dung trả về)
        }

        // POST: api/Users - Thêm người dùng mới
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                // Kiểm tra dữ liệu nhập
                if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                {
                    return BadRequest("Username and Password are required.");
                }

                // Kiểm tra trùng lặp Username hoặc Email
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    return Conflict("Username is already taken.");
                }

                if (!string.IsNullOrEmpty(user.Email) && _context.Users.Any(u => u.Email == user.Email))
                {
                    return Conflict("Email is already registered.");
                }

                // Mã hóa mật khẩu
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Gán thời gian tạo
                user.CreatedAt = DateTime.UtcNow;
                user.IsDeleted = false;

                // Lưu dữ liệu vào database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Trả về kết quả thành công
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                // Ghi lại log lỗi
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }






        // DELETE: api/Users/5 - Xóa người dùng theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id); // Tìm người dùng theo ID
            if (user == null) // Nếu không tìm thấy, trả về 404 NotFound
            {
                return NotFound();
            }

            _context.Users.Remove(user); // Xóa người dùng khỏi DbContext
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            return NoContent(); // Trả về 204 NoContent
        }

        // Phương thức kiểm tra người dùng có tồn tại trong cơ sở dữ liệu không
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id); // Trả về true nếu người dùng tồn tại
        }
    }
}
