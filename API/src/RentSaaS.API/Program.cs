using System.Text;
using System.Reflection;
using RentSaaS.API.Extensions;
using RentSaaS.Domain.Entities;
using FluentValidation.AspNetCore;
using RentSaaS.Infrastructure.Data;  
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//---------------- JWT Configuration
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<RentSaaSDBContext>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Infrastructure Services
builder.Services.AddInfrastructureServices(builder.Configuration);

//------------------------- Add CORS
builder.Services.AddCors(o =>
{
    o.AddPolicy("MyPolicey", x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
}
);

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





builder.Services.AddAuthentication().AddGoogle();





builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ; 
 
var app = builder.Build();



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
