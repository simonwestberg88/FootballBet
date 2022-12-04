namespace FootballBet.Shared.Models.Stats;

public readonly record struct ChartResponse
(
    IEnumerable<UserData> UserData
);

public readonly record struct UserData
(
    string Username,
    IEnumerable<WinData> Wins 
);
public readonly record struct WinData
(
    DateTime Date,
    decimal Amount
);