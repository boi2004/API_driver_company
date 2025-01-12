
using Driver_Company_5._0.Modules.Authentication.Models;
using Driver_Company_5._0.Modules.Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace Driver_Company_5._0.Modules.Authentication.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Dữ liệu không hợp lệ.", Errors = GetModelErrors() });

            // Kiểm tra định dạng đầu vào
            if (!IsValidInput(request, out var validationErrors))
                return BadRequest(new { Message = "Dữ liệu không hợp lệ.", Errors = validationErrors });

            var response = await _registerService.RegisterUser(request);
            if (!response.Success)
                return BadRequest(new { Message = response.Message });

            return Ok(new { Message = "Đăng ký thành công!" });
        }

        private bool IsValidInput(RegisterRequest request, out List<string> errors)
        {
            errors = new List<string>();

            if (!IsValidName(request.FirstName)) errors.Add("Họ không hợp lệ.");
            if (!IsValidName(request.LastName)) errors.Add("Tên không hợp lệ.");
            if (!IsValidUsername(request.Username)) errors.Add("Tên đăng nhập phải có ít nhất 5 ký tự và không chứa ký tự đặc biệt.");
            if (!IsValidPhoneNumber(request.PhoneNumber)) errors.Add("Số điện thoại không hợp lệ. Chỉ chấp nhận 10-11 chữ số.");
            if (!IsValidEmail(request.Email)) errors.Add("Email không hợp lệ.");
            if (!IsValidPassword(request.Password)) errors.Add("Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.");
            if (request.Password != request.ConfirmPassword) errors.Add("Mật khẩu xác nhận không khớp.");
            if (!IsValidBirthday(request.Birthday)) errors.Add("Ngày sinh không hợp lệ hoặc chưa đủ 18 tuổi.");

            return !errors.Any();
        }

        private bool IsValidUsername(string username) => Regex.IsMatch(username, @"^[a-zA-Z0-9]{5,}$");
        private bool IsValidPhoneNumber(string phoneNumber) => Regex.IsMatch(phoneNumber, @"^\d{10,11}$");
        private bool IsValidBirthday(DateTime birthday) => DateTime.Now.Year - birthday.Year >= 18;
        private bool IsValidName(string name) => Regex.IsMatch(name, @"^[a-zA-ZÀ-Ỹà-ỹ\s]+$");
        private bool IsValidPassword(string password) => Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        private bool IsValidEmail(string email) => Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");

        private List<string> GetModelErrors()
        {
            return ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }
    }
}
