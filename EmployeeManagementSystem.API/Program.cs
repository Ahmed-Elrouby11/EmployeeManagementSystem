using EmployeeManagementSystem.API.Middleware;
using EmployeeManagementSystem.Application.Interfaces.Services;
using EmployeeManagementSystem.Application.Mappings;
using EmployeeManagementSystem.Application.Validators.Employee;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Infrastructure.Persistence.Context;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Asp.Versioning;
using Serilog;
using System.Text;


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog();
// =============================
// Database
// =============================
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// =============================
// Infrastructure
// =============================
builder.Services.AddInfrastructureServices();

// =============================
// AutoMapper
// =============================
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// =============================
// FluentValidation
// =============================
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();

// =============================
// Dependency Injection
// =============================
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// =============================
// Identity
// =============================
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// =============================
// JWT Settings
// =============================
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtSettings>();

// =============================
// JWT Authentication
// =============================
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings!.Issuer,
                ValidAudience = jwtSettings.Audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
    });

// =============================
// Controllers
// =============================
builder.Services.AddControllers();

// =============================
// Swagger
// =============================
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Management System API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);

    options.AssumeDefaultVersionWhenUnspecified = true;

    options.ReportApiVersions = true;

    options.ApiVersionReader =
        new UrlSegmentApiVersionReader();
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManageEmployees", policy =>
    {
        policy.RequireRole("Admin", "HR");
    });

    options.AddPolicy("ViewEmployees", policy =>
    {
        policy.RequireRole("Admin", "HR", "Manager", "Employee");
    });

    options.AddPolicy("ManageDepartments", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("ManageUsers", policy =>
    {
        policy.RequireRole("Admin");
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging();

// =============================
// Middleware Pipeline
// =============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Authentication must come before Authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    await RoleSeeder.SeedRolesAsync(roleManager);
}

app.Run();