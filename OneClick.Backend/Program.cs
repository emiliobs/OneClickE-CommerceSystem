using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using OneClick.Backend.Data;
using OneClick.Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// -----DATABASE CONNECTION LOGIC -----
//string connectionStringName = "OneClickConnection";

//if (builder.Environment.IsDevelopment())
//{
//    // If running locally in Visual Studio
//    connectionStringName = "OneClickConnectionLocal";
//}
//else
//{
//    // If running on the Server (SmarterASP / Azure)
//    connectionStringName = "OneClickConnection";
//}

//var connectionString = builder.Configuration.GetConnectionString(connectionStringName);

builder.Services.AddDbContext<OneClickDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "OneClickConnection"));
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow requests from anywhere (Cloud, Localhost, etc.)
              .AllowAnyMethod()  // Allow GET, POST, PUT, DELETE
              .AllowAnyHeader(); // Allow any headers
    });
});

var app = builder.Build();

// Aqui para mostar los endpoints de swagger a nivel de produccion
app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();

//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// 2. Activate CORS Middleware (MUST be before UseAuthorization)
app.UseCors("AllowAll"); // <--- This opens the door!

app.UseAuthorization();

app.MapControllers();

app.Run();