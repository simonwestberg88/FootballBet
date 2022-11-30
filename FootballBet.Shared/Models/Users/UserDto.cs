using FootballBet.Shared.Models.Stats;

namespace FootballBet.Shared.Models.Users;

public readonly record struct UserDto
(
    string Id,
    string UserName,
    string Email,
    decimal Balance,
    BetStatsDto? Stats
);