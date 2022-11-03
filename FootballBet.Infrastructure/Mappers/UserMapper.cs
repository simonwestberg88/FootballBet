using FootballBet.Repository.Entities;
using FootballBet.Server.Models;

namespace FootballBet.Infrastructure.Mappers;

public static class UserMapper
{
    public static Shared.Models.Users.UserDto Map(ApplicationUser databaseUser)
        => new ()
        {
            Id = databaseUser.Id,
            Email = databaseUser.Email,
            UserName = databaseUser.UserName,
            Balance = databaseUser.Balance
        };
}