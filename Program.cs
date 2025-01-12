// File: Program.cs
using Driver_Company_5._0.Infrastructure.Data;
using Driver_Company_5._0.Modules.Authentication.Security;
using Driver_Company_5._0.Modules.Authentication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext với chuỗi kết nối từ appsettings.json
builder.Services.AddDbContext<DriverManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("driver_management")));

// Đăng ký LoginService và JwtHelper vào Dependency Injection (DI)
// Toàn bộ các controller sẽ bỏ vào này
builder.Services.AddScoped<LoginService>();
builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddScoped<RegisterService>();

// Cấu hình Authentication cho JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Thêm Swagger có cấu hình bảo mật cho JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Driver Management API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Nhập token vào đây: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Khởi tạo ứng dụng
var app = builder.Build();

// Middleware xử lý lỗi
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Thêm middleware cho Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Driver Management API v1");
    c.RoutePrefix = string.Empty; // Swagger sẽ truy cập qua đường dẫn /swagger
});

// Thêm middleware cho Authentication và Authorization
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map các controller
app.MapControllers();

// Chạy ứng dụng
app.Run();