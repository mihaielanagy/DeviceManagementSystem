global using DeviceManagementDB.Models;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Text;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ILoggingService, LoggingService>();

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IDevicesService, DevicesService>();

builder.Services.AddTransient<IDataService<CityDto>, CitiesService>();
builder.Services.AddTransient<IDataService<Country>, CountriesService>();
builder.Services.AddTransient<IDataService<DeviceType>, DeviceTypesService>();
builder.Services.AddTransient<IDataService<Manufacturer>, ManufacturersService>();
builder.Services.AddTransient<IDataService<OperatingSystem>, OperatingSystemsService>();
builder.Services.AddTransient<IDataService<Processor>, ProcessorsService>();
builder.Services.AddTransient<IDataService<Ramamount>, RamAmountsService>();
builder.Services.AddTransient<IDataService<Role>, RolesService>();
builder.Services.AddTransient<IDataService<OsVersionDto>, OSVersionsService>();
builder.Services.AddTransient<IDataService<LocationDto>, LocationsService>();

builder.Services.AddTransient<IBaseRepository<City>, BaseRepository<City>>();
builder.Services.AddTransient<IBaseRepository<Country>, BaseRepository<Country>>();
builder.Services.AddTransient<IBaseRepository<DeviceType>, BaseRepository<DeviceType>>();
builder.Services.AddTransient<IBaseRepository<Manufacturer>, BaseRepository<Manufacturer>>();
builder.Services.AddTransient<IBaseRepository<OperatingSystem>, BaseRepository<OperatingSystem>>();
builder.Services.AddTransient<IBaseRepository<Processor>, BaseRepository<Processor>>();
builder.Services.AddTransient<IBaseRepository<Ramamount>, BaseRepository<Ramamount>>();
builder.Services.AddTransient<IBaseRepository<Role>, BaseRepository<Role>>();
builder.Services.AddTransient<IBaseRepository<OperatingSystemVersion>, BaseRepository<OperatingSystemVersion>>();
builder.Services.AddTransient<IBaseRepository<Location>, BaseRepository<Location>>();
builder.Services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();

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
