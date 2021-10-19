using Duende.IdentityServer.EntityFramework.Options;
using FootballBet.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FootballBet.Server.Models.Groups;

namespace FootballBet.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<BettingGroup> BettingGroups { get; set; }
        public DbSet<BettingGroupMember> BettingGroupMembers {  get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}