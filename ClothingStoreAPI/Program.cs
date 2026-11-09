using ClothingStoreAPI.Data;
using ClothingStoreAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Подключение БД
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=app.db"));

// 2. Игнорирование циклов в JSON (ИЗ ЗАДАНИЯ)
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// 3. Настройка JWT
var jwtKey = "super_secret_key_that_is_long_tefteluus_12345";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// 4. Разрешаем CORS (чтобы Swagger точно не ругался)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Убираем app.UseHttpsRedirection(); чтобы избежать ошибок в Swagger!

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// АВТОМАТИЧЕСКОЕ СОЗДАНИЕ МЕНЕДЖЕРА ПРИ ЗАПУСКЕ
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // Создаем БД, если ее нет
    if (!context.Users.Any(u => u.Role == "Manager"))
    {
        context.Users.Add(new User
        {
            Login = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Phone = "123",
            Address = "Admin",
            Role = "Manager"
        });
        context.SaveChanges();
    }
}

app.Run();