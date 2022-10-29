using Duende.IdentityServer.EntityFramework.Options;
using FootballBet.Server.Models;
using FootballBet.Server.Models.Football.DBModels;
using FootballBet.Server.Models.Groups;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<BettingGroup> BettingGroups { get; set; }
        public DbSet<BettingGroupMember> BettingGroupMembers { get; set; }
        public DbSet<BettingGroupInvitation> BettingGroupInvitations { get; set; }
        public DbSet<MatchEntity> MatchEntities { get; set; }
        public DbSet<LeagueEntity> LeagueEntities { get; set; }
        public DbSet<TeamEntity> TeamEntities { get; set; }



        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }
    }
}