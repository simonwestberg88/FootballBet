using FootballBet.Infrastructure;
using FootballBet.Infrastructure.Http;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Settings;
using FootballBet.Repository;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories;
using FootballBet.Server.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestProject.TestApi;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
builder.Services.Configure<FootballApiSettings>(builder.Configuration.GetSection("FootballApi"));
builder.Services.AddSingleton<IFootballApiClient, FootballApiClient>();
builder.Services.AddHttpClient<FootballApiClient>();
builder.Services.AddScoped<IFootballRepository, FootballRepository>();
builder.Services.AddScoped<IFootballAPIService, FootballApiService>();
var app = builder.Build();
app.AddTestApi();

app.Run();