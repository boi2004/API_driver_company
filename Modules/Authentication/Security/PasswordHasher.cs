using BCrypt.Net;

namespace Driver_Company_5._0.Modules.Authentication.Security
{
    public static class PasswordHasher
    {
        // Mã hóa mật khẩu
        public static string HashPassword(string password, int workFactor = 12)  //workFactor = 12: Tăng số vòng lặp để cải thiện độ mạnh của mã hóa, mặc dù sẽ chậm hơn.

        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        }


        // Xác thực mật khẩu
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
