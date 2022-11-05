using FootballBet.Infrastructure.Http;
using FootballBet.Infrastructure.Interfaces;
using FootballBet.Infrastructure.Services;
using FootballBet.Repository.Repositories;
using FootballBet.Repository.Repositories.Interfaces;
using FootballBet.Server.Data.Repositories.Interfaces;
using FootballBet.Server.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FootballBet.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFootballBetServices(this IServiceCollection services)
        => services.AddTransient<IEmailSender, EmailSender>()
            
            .AddTransient<IFootballApiClient, FootballApiClient>()
            .AddTransient<IFootballAPIService, FootballApiService>()
            .AddTransient<IFootballRepository, FootballRepository>()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<ITransactionService, TransactionService>()
            .AddTransient<IBetService, BetService>()
            .AddRepositories();

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services.AddTransient<IGroupRepository, GroupRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IGroupService, GroupService>()
            .AddTransient<IOddsRepository, OddsRepository>()
            .AddTransient<IMatchRepository, MatchRepository>()
            .AddTransient<IBetRepository, BetRepository>();
}