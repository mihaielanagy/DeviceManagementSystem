global using DeviceManagementDB.Models;
using DeviceManagementWeb.Services;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ILoggingService, LoggingService>();
builder.Services.AddTransient<IRolesService, RolesService>();
builder.Services.AddTransient<IRamAmountsService, RamAmountsService>();
builder.Services.AddTransient<IProcessorsService, ProcessorsService>();
builder.Services.AddTransient<IManufacturersService, ManufacturersService>();
builder.Services.AddTransient<IDeviceTypesService, DeviceTypesService>();
builder.Services.AddTransient<ICountriesService, CountriesService>();
builder.Services.AddTransient<ICitiesService, CitiesService>();
builder.Services.AddTransient<ILocationService, LocationsService>();
builder.Services.AddTransient<IOperatingSystemsService, OperatingSystemsService>();
builder.Services.AddTransient<IOsVersionService, OSVersionsService>();

// Add services to the container.
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "https://localhost:7250",
        ValidAudience = "https://localhost:7250",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@123"))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy(name: "DeviceOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddDbContext<DeviceManagementContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DeviceOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
