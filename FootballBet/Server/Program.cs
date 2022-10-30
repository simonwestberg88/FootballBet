using FootballBet.Server.Data;
using FootballBet.Server.Data.Repositories;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Data.Services;
using FootballBet.Server.Data.Services.APIs;
using FootballBet.Server.Data.Services.Interfaces;
using FootballBet.Server.Data.Settings;
using FootballBet.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.Configure<FootballApiSettings>(builder.Configuration.GetSection("FootballApi"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IFootballApiClient, FootballApiClientClient>();
builder.Services.AddTransient<IFootballAPIService, FootballAPIService>();
builder.Services.AddTransient<IFootballRepository, FootballRepository>();
builder.Services.AddHttpClient<IFootballApiClient, FootballApiClientClient>(client =>
{
    var baseUrl = builder.Configuration.GetSection("FootballApi")["url"];
    var key = builder.Configuration.GetSection("FootballApi")["key"];
    var host = builder.Configuration.GetSection("FootballApi")["host"];
    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("x-rapidapi-key", key);
    client.DefaultRequestHeaders.Add("x-rapidapi-host", host);
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
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
//app.UseHangfireServer();
//app.UseHangfireDashboard("/hangfire", new DashboardOptions
//{
//    //Authorization = new [] {new HangfireAuthorization()} //can ensure that this can only be visited by people with a certain role associated to them
//});

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
