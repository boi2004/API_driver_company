namespace Driver_Company_5._0.Modules.Authentication.Models
{
    public class RegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty ;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }

        
    }
}
