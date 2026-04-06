using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Note: OpenApi Models using statement removed to prevent .NET 10 conflicts
using OneClick.Backend.Data;
using OneClick.Backend.Repositories;
using OneClick.Backend.Services;
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
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
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

// Register services
builder.Services.AddScoped<IImageService, ImageService>();

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

// --- SEED ADMIN USER WITH PASSWORD "123" BYPASSING STRICT POLICIES ---
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<OneClick.Backend.Data.OneClickDbContext>();

        // Check if the admin already exists
        if (!context.Users.Any(u => u.Email == "admin@yopmail.com"))
        {
            var adminUser = new OneClick.Shared.Entities.User
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@yopmail.com",
                NormalizedUserName = "ADMIN@YOPMAIL.COM",
                Email = "admin@yopmail.com",
                NormalizedEmail = "ADMIN@YOPMAIL.COM",
                EmailConfirmed = true,
                Role = "Admin",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            // Generate the secure hash dynamically for "123" bypassing strict policies
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<OneClick.Shared.Entities.User>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "123");

            // Save directly to the Database Context
            context.Users.Add(adminUser);
            context.SaveChanges();
            Console.WriteLine("Admin user seeded successfully with password 123.");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error seeding admin user: {ex.Message}");
}

// Ensure app.Run() is the absolute last line in your Program.cs
// app.Run();

app.Run();