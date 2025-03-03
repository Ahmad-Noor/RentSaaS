using Serilog;
using System.Text;
using RentSaaS.API.Extensions;
using RentSaaS.Domain.Entities;
using RentSaaS.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RentSaaS.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FileUploadSettings>(builder.Configuration.GetSection("FileUploadSettings"));

//---------------- JWT Configuration
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<RentSaaSDBContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();
 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Infrastructure Services
builder.Services.AddInfrastructureServices(builder.Configuration);

//------------------------- Add CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//-------------------------Add Rate Limiter
//TODO: Add Rate Limiter

//------------------------- Logger
string logPath = builder.Configuration.GetSection("Logging:LogPath").Value;
if (!string.IsNullOrWhiteSpace(logPath))
{
    var _logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File(logPath)
        .CreateLogger();
    builder.Logging.AddSerilog(_logger);
}
else
{
    throw new InvalidOperationException("Log path is not configured.");
}

//------------------------- Add Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key is not configured");
}

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = "https://accounts.google.com";
        options.Audience = builder.Configuration["1000319891618-fnnc0set8ng1rrrke3hujb67cd6cpb5u.apps.googleusercontent.com"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError("Authentication failed: {Error}", context.Exception);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Token validated successfully");
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Auth header: {Header}", context.Request.Headers["Authorization"].ToString());
                return Task.CompletedTask;
            }
        };
    });





builder.Services.AddAuthentication().AddGoogle(optionsGoogle =>
{
    optionsGoogle.ClientId = "1000319891618-fnnc0set8ng1rrrke3hujb67cd6cpb5u.apps.googleusercontent.com";

    optionsGoogle.ClientSecret = "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";
    optionsGoogle.CallbackPath = "/signin-google"; // Ensure this matches your Google Console settings
});






builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

}); ;



var app = builder.Build();

//Enable CORS
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

    if (context.User.Identity?.IsAuthenticated ?? false)
    {
        logger.LogInformation("User is authenticated: {User}", context.User.Identity.Name);
    }
    else
    {
        logger.LogWarning("User is not authenticated");
        var token = context.Request.Headers["Authorization"].ToString();
        logger.LogInformation("Authorization header: {Token}", token);
    }

    await next();
});



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseStaticFiles();
// app.UseRateLimiter();
// app.UseRequestLocalization();
// app.UseCors();
// app.UseCookiePolicy();
app.UseRouting();
app.UseCors("MyPolicey");
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
