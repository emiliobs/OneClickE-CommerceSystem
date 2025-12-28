using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using OneClick.Backend.Data;
using OneClick.Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OneClickDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("OneClickConnection"));
});

// Register repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

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
        builder.WithOrigins("https://localhost:5001", "http://localhost:5000").AllowAnyHeader().AllowAnyMethod();
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
            Email = "OneClickSupport@yopmail.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();