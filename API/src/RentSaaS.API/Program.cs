using Serilog;
using System.Text;
using System.Reflection;
using RentSaaS.Domain.Entities;
using FluentValidation.AspNetCore;
using RentSaaS.API.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RentSaaS.Infrastructure.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//---------------- JWT Configuration
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<RentSaaSDBContext>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();

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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var SecurectKey =Encoding.ASCII.GetBytes( builder.Configuration.GetSection("Jwt:Key").Value);
    //options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //ValidateIssuer = true,
        //ValidateAudience = true,
        //ValidateLifetime = true,
        //ValidateIssuerSigningKey = true,
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(SecurectKey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        RequireExpirationTime = false,
        //ClockSkew = TimeSpan.Zero

    };
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });






var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
