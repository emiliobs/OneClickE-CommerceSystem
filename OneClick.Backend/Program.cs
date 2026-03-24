using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Note: OpenApi Models using statement removed to prevent .NET 10 conflicts
using OneClick.Backend.Data;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ----- DATABASE CONNECTION LOGIC -----
builder.Services.AddDbContext<OneClickDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "OneClickConnection"));
});

// ----- IDENTITY CONFIGURATION -----
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
})
.AddEntityFrameworkStores<OneClickDbContext>()
.AddDefaultTokenProviders();

// ----- JWT AUTHENTICATION CONFIGURATION -----
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// Register repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Handle circular references
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    // Pretty print JSON in developer
    options.JsonSerializerOptions.WriteIndented = builder.Environment.IsDevelopment();
});

// Enable API Explorer for Swagger
builder.Services.AddEndpointsApiExplorer();

// ----- UPDATED: SIMPLIFIED SWAGGER GEN -----
// We removed the complex security definitions to bypass the version bug.
// The API is perfectly secure, we just won't have the login button inside Swagger.
builder.Services.AddSwaggerGen();

// Add CORS Policy Service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Activate CORS Middleware
app.UseCors("AllowAll");

// ACTIVATE AUTHENTICATION (Must be before Authorization)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();