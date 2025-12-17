using FoodOrderingApplicationFigma.Data;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Interfaces;
using FoodOrderingApplicationFigma.Repository;
using FoodOrderingApplicationFigma.Repository.Repositories;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using FoodOrderingApplicationFigma.Services;
using FoodOrderingApplicationFigma.Services.Service_Folder;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using FoodOrderingApplicationFigma.Services.JwtService;
using FoodOrderingApplicationFigma.Mappings;
using FoodOrderingApplicationFigma.Middleware;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// Logging (important for Render)
// --------------------------------------------------
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// --------------------------------------------------
// Connection String (Local + Render)
// --------------------------------------------------
var connectionString =
    builder.Configuration.GetConnectionString("DbConn")
    ?? Environment.GetEnvironmentVariable("DbConn");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DbConn' not found.");
}

// --------------------------------------------------
// DbContext
// --------------------------------------------------
builder.Services.AddDbContext<FoodOrderingDbContext>(options =>
    options.UseSqlServer(connectionString));

// --------------------------------------------------
// AutoMapper
// --------------------------------------------------
builder.Services.AddAutoMapper(typeof(MappingProfile));

// --------------------------------------------------
// Repositories & Services
// --------------------------------------------------
builder.Services.AddScoped<IUsers<User>, UserRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IStateService, StateService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// --------------------------------------------------
// OTP / Email / SMS
// --------------------------------------------------
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHttpClient<ISmsService, SmsService>();

// --------------------------------------------------
// CORS (ALLOW ALL – SAFE FOR API)
// --------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// --------------------------------------------------
// JWT Authentication
// --------------------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]
                    ?? Environment.GetEnvironmentVariable("Jwt__Key")!
                )
            )
        };
    });

// --------------------------------------------------
// Controllers + JSON
// --------------------------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --------------------------------------------------
// Middleware ORDER (CRITICAL)
// --------------------------------------------------
app.UseRouting();

// Swagger (enabled everywhere)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Food Ordering API");
});

// Global middlewares
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseCors("AllowAll");

// HTTPS only for local
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
