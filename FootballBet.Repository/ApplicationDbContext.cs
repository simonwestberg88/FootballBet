using Duende.IdentityServer.EntityFramework.Options;
using FootballBet.Repository.Entities;
using FootballBet.Server.Models;
using FootballBet.Server.Models.Football.DBModels;
using FootballBet.Server.Models.Groups;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FootballBet.Repository;

public class ApplicationDbContext: ApiAuthorizationDbContext<ApplicationUser>
{
    public DbSet<BettingGroupEntity> BettingGroups => Set<BettingGroupEntity>();
    public DbSet<BettingGroupMemberEntity> BettingGroupMembers => Set<BettingGroupMemberEntity>();
    public DbSet<BettingGroupInvitationEntity> BettingGroupInvitations => Set<BettingGroupInvitationEntity>();
    public DbSet<MatchEntity> MatchEntities => Set<MatchEntity>();
    public DbSet<LeagueEntity> LeagueEntities => Set<LeagueEntity>();
    public DbSet<TeamEntity> TeamEntities => Set<TeamEntity>();



    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
}