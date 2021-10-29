using FootballBet.Server.Models;

namespace FootballBet.Server.Data.Mappers
{
    public static class UserMapper
    {
        public static Shared.Models.Users.User Map(ApplicationUser databaseUser)
            => new ()
            {
                Id = databaseUser.Id,
                Email = databaseUser.Email,
                UserName = databaseUser.UserName
            };
    }
}
