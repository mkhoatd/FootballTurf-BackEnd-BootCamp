using Microsoft.AspNetCore.Identity;
using Serilog;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Extensions;
using WebApi.Persistence;
using WebApi.Hubs;
using WebApi.Repository.Helpers;
using WebApi.Infrastructure.Midlleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
// Add services to the container.
var configuration = new ConfigurationBuilder();

configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsetting.Production.json", optional: true);

IConfigurationRoot config = configuration.Build();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();

builder.Services.AddSignalR();

builder.Services.ConnectDBService(builder.Configuration);

builder.Services.AddCorsService(config);

builder.Services.AddApplicationServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));

builder.Services
    .AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
