namespace FootballBet.Shared.Models.Stats;

public readonly record struct WinStatsResponse(IEnumerable<WinStats> WinStats);

public readonly record struct WinStats
(
    string NickName,
    decimal WinAmount,
    bool IsExactWin,
    DateTime Date
);
        
