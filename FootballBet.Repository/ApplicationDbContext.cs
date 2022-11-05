using Duende.IdentityServer.EntityFramework.Options;
using FootballBet.Repository.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FootballBet.Repository;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public DbSet<BettingGroupEntity> BettingGroups => Set<BettingGroupEntity>();
    public DbSet<BettingGroupMemberEntity> BettingGroupMembers => Set<BettingGroupMemberEntity>();
    public DbSet<BettingGroupInvitationEntity> BettingGroupInvitations => Set<BettingGroupInvitationEntity>();
    public DbSet<MatchEntity> MatchEntities => Set<MatchEntity>();
    public DbSet<LeagueEntity> LeagueEntities => Set<LeagueEntity>();
    public DbSet<TeamEntity> TeamEntities => Set<TeamEntity>();
    public DbSet<BetEntity> BetEntities => Set<BetEntity>();
    public DbSet<OddsEntity> OddsEntities => Set<OddsEntity>();
    public DbSet<OddsGroupEntity> OddsGroupEntities => Set<OddsGroupEntity>();


    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
}