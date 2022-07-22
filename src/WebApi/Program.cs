global using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApi.Infrastructure.Extensions;
using WebApi.Hubs;
using WebApi.Repository.Helpers;
using WebApi.Infrastructure.Midlleware;
using WebApi.Persistence;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
var config = builder.Configuration;

// Add services to the container.
var connectionString = config.GetConnectionString("KMShootDB");
builder.Services.AddDbContext<AppFootballTurfDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(config));

builder.Services.AddSignalR();

builder.Services.AddCorsService(config);

builder.Services.AddApplicationServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));

builder.Services
    .AddControllers()
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        option.JsonSerializerOptions.PropertyNamingPolicy=JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseCors("CorsPolicyHttpAndHttps");
app.MapControllers();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ConnectionHub>("/chatHub");
});

app.Run();
