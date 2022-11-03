using FootballBet.Repository.Entities;
using FootballBet.Shared.Models.Users;

namespace FootballBet.Infrastructure.Mappers;

public static class UserMapper
{
    public static UserDto ToUserDto(this ApplicationUser databaseUser)
        => new ()
        {
            Id = databaseUser.Id,
            Email = databaseUser.Email,
            UserName = databaseUser.UserName,
            Balance = databaseUser.Balance
        };
}