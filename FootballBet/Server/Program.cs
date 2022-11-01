using FootballBet.Infrastructure;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Settings;
using FootballBet.Repository;
using FootballBet.Repository.Entities;
using FootballBet.Repository.Repositories;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Controllers;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Data.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("FootballBet.Repository")));
builder.Services.Configure<FootballApiSettings>(builder.Configuration.GetSection("FootballApi"));
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("EmailSenderSettings"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IFootballApiClient, FootballApiClient>();
builder.Services.AddTransient<IFootballAPIService, FootballApiService>();
builder.Services.AddTransient<IFootballRepository, FootballRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();
app.AddMatchesApi();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
