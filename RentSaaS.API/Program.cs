using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.RateLimiting; 
using RentSaaS.API.ServiceExtension;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
 
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

// Add services to the container.
 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddDatabaseServices(builder.Configuration);
//-------------------------Add Rate Limiter
//TODO: Add Rate Limiter

//------------------------- enable Cors
//TODO: add Cors
builder.Services.AddCors();

//-------------------------Logger
string LogPath = builder.Configuration.GetSection("Logging:LogPath").Value;
var _logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(LogPath)
    .CreateLogger();
builder.Logging.AddSerilog(_logger);
//------------------------------------

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 

app.UseHttpsRedirection();
app.UseStaticFiles();
// app.UseCookiePolicy();

app.UseRouting();
// app.UseRateLimiter();
// app.UseRequestLocalization();
// app.UseCors();


app.UseAuthentication();
app.UseAuthorization();
// app.UseSession();
// app.UseResponseCompression();
// app.UseResponseCaching();
 
 
app.MapControllers();
 
app.Run();
