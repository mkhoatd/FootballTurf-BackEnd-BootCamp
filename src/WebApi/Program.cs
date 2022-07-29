global using System.Text.Json;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
using WebApi.Infrastructure.Extensions;
using WebApi.Hubs;
using WebApi.Repository.Helpers;
using WebApi.Infrastructure.Midlleware;
using WebApi.Persistence;
using WebApi.Extensions;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
var config = builder.Configuration;

// Add services to the container.
var connectionString = config.GetConnectionString("FootballTurfDB");

if (Environment.GetEnvironmentVariable("CONNECTION_STRING") is not null)
{
    connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
}

builder.Services.AddDbContext<AppFootballTurfDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(config));

builder.Services.AddSignalR();

builder.Services.AddCorsService(config);

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationServices();

builder.Services.AddBusinessServices();

builder.Services.AddJwtService(config);

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));

builder.Services
    .AddControllers()
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppFootballTurfDbContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedData(context);
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred while migrating or seeding the database.");
    }
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseCors("CorsPolicyHttpAndHttps");
app.MapControllers();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapHub<ConnectionHub>("/chatHub"));

//run app at https://localhost:7045



app.Run();
