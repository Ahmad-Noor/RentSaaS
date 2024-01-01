using Common.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentSaaS.API.Configurations;
using RentSaaS.API.ServiceExtension;
using RentSaaS.Common;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//---------------- JWT Config
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwrConfig"));
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ConfigurationDBContext>();



builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
 
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddRentSaaSContext(builder.Configuration);


//-------------------------Add Rate Limiter
//TODO: Add Rate Limiter

//------------------------- enable Cors
//TODO: add Cors
//builder.Services.AddCors();

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


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var SecurectKey =Encoding.ASCII.GetBytes( builder.Configuration.GetSection("JwtConfig:SecretKey").Value);
    //options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(SecurectKey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        RequireExpirationTime = false,
        //ClockSkew = TimeSpan.Zero
    };
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
