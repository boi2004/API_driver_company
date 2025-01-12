using Driver_Company_5._0.Modules.Authentication.Controller;
using Driver_Company_5._0.Modules.Authentication.Models;

namespace Driver_Company_5._0.Modules.Authentication.Router
{
    public static class LoginRoutes
    {
        /// Định nghĩa các endpoint liên quan đến Authentication
        public static void MapAuthRoutes(this IEndpointRouteBuilder endpoints)
        {
            // Định nghĩa các route cho module Auth
            // Endpoint đăng nhập
            endpoints.MapPost("/api/auth/login", async (LoginController controller, LoginRequest request)
                => await controller.Login(request))
                .WithName("Login")
                .WithMetadata(new RouteNameMetadata("Login")); // Thêm metadata cho route này
        }
    }
}
