using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OneClick.Backend.Data;
using OneClick.Backend.Repositories;
using OneClick.Backend.Services;
using OneClick.Shared.Entities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ----- DATABASE CONNECTION -----
builder.Services.AddDbContext<OneClickDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OneClickConnection"));
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

// ----- JWT AUTHENTICATION -----
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

// Register repositories and services
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Pipeline configuration
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ----- SEED ADMIN USER AND 20 ORDERS -----
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<OneClickDbContext>();

        // Ensure Database is updated
        context.Database.Migrate();

        // 1. Check if Admin User exists
        var adminEmail = "admin@yopmail.com";
        var adminUser = context.Users.FirstOrDefault(u => u.Email == adminEmail);

        if (adminUser == null)
        {
            adminUser = new User
            {
                FirstName = "Emilio Antonio",
                LastName = "Barrera Sepulveda",
                UserName = adminEmail,
                NormalizedUserName = adminEmail.ToUpper(),
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpper(),
                EmailConfirmed = true,
                Role = "Admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<User>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "123");

            context.Users.Add(adminUser);
            context.SaveChanges(); // Save to get the actual User ID
            Console.WriteLine("Admin user created.");
        }

        // 2. Check if Orders exist for this user
        if (!context.Orders.Any())
        {
            var staticDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Add 20 Orders linked to the adminUser.Id
            var orders = new List<Order>();
            for (int i = 1; i <= 20; i++)
            {
                orders.Add(new Order
                {
                    UserId = adminUser.Id,
                    OrderDate = staticDate.AddDays(i), // Incremental dates
                    TotalAmount = 50.00m + (i * 10.50m),
                    OrderStatus = "Pending",
                    ShippingName = $"{adminUser.FirstName} {adminUser.LastName}",
                    ShippingPhone = "987654321",
                    ShippingAddress = $"Avenida Siempreviva {i}",
                    City = "Santiago",
                    Postcode = "123456"
                });
            }

            context.Orders.AddRange(orders);
            context.SaveChanges();
            Console.WriteLine("20 sample orders seeded successfully.");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error during manual seeding: {ex.Message}");
}

app.Run();