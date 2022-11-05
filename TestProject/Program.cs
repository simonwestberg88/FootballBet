using System.Text.Json.Serialization;
using FootballBet.Infrastructure;
using FootballBet.Infrastructure.Http;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Settings;
using FootballBet.Repository;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using TestProject.TestApi;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
builder.Services.Configure<FootballApiSettings>(builder.Configuration.GetSection("FootballApi"));
builder.Services.AddScoped<IFootballApiClient, FootballApiClient>();
builder.Services.AddHttpClient<FootballApiClient>();
builder.Services.AddScoped<IFootballRepository, FootballRepository>();
builder.Services.AddScoped<IFootballAPIService, FootballApiService>();
builder.Services.AddScoped<IOddsRepository, OddsRepository>();
var app = builder.Build();
app.AddTestApi();

app.Run();