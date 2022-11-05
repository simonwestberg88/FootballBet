using FootballBet.Infrastructure.Settings;
using FootballBet.Repository;
using FootballBet.Repository.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballBet.Infrastructure.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    public static void AddFootballBetDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("FootballBet.Repository")));
        builder.Services.Configure<FootballApiSettings>(builder.Configuration.GetSection("FootballApi"));
        builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("EmailSenderSettings"));

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
    }

    public static void AddFootballBetConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<FootballApiSettings>(builder.Configuration.GetSection("FootballApi"));
    }
}