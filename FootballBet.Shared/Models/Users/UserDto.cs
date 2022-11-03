namespace FootballBet.Shared.Models.Users;

public readonly record struct UserDto
(
    string Id,
    string UserName,
    string Email,
    decimal Balance
);