using FootballBet.Repository.Entities;
using FootballBet.Server.Models;

namespace FootballBet.Infrastructure.Mappers;

public static class UserMapper
{
    public static FootballBet.Shared.Models.Users.User Map(ApplicationUser databaseUser)
        => new ()
        {
            Id = databaseUser.Id,
            Email = databaseUser.Email,
            UserName = databaseUser.UserName
        };
}