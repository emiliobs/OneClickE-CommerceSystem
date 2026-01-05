using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using OneClick.Backend.Data;
using OneClick.Backend.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// -----DATABASE CONNECTION LOGIC -----
string connectionStringName;

if (builder.Environment.IsDevelopment())
{
    // If running locally in Visual Studio
    connectionStringName = "OneClickConnectionLocal";
}
else
{
    //// If running on the Server (SmarterASP / Azure)
    connectionStringName = "OneClickConnection";
}

//var connectionString = builder.Configuration.GetConnectionString(connectionStringName);

builder.Services.AddDbContext<OneClickDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionStringName));
});

// Register repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Handle circular references
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

    // Pretty print JSON in developer
    options.JsonSerializerOptions.WriteIndented = builder.Environment.IsDevelopment();
});

// Add CORS for blazorforntend (we will add this later)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorFrontend", builder =>
    {
        builder.WithOrigins("https://localhost:7171")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OneClick E-Commerce API",
        Version = "v1",
        Description = "API for OneClick E-Commerce System",
        Contact = new OpenApiContact
        {
            Name = "OneClick Team",
            Email = "oneclick.project.emilio@outlook.com"
        }
    });
});

// 1. Add CORS Policy Service
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()  // Allow requests from anywhere (Cloud, Localhost, etc.)
//              .AllowAnyMethod()  // Allow GET, POST, PUT, DELETE
//              .AllowAnyHeader(); // Allow any headers
//    });
//});

var app = builder.Build();

// Aqui para mostar los endpoints de swagger a nivel de produccion
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 2. Activate CORS Middleware (MUST be before UseAuthorization)
//app.UseCors("AllowAll"); // <--- This opens the door!

app.UseCors("AllowBlazorFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();