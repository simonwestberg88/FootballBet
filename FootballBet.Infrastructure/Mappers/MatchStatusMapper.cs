using FootballBet.Repository.Entities;

namespace FootballBet.Infrastructure.Mappers;

public static class MatchStatusMapper
{
    public static string GetStatusString(this MatchStatus status)
        => status switch
        {
            MatchStatus.TBD => "TBD",
            MatchStatus.NS => "Not started",
            MatchStatus.FH => "First half",
            MatchStatus.HT => "Half time",
            MatchStatus.SH => "Second Half",
            MatchStatus.ET => "Extra Time",
            MatchStatus.P => "Penalty",
            MatchStatus.FT => "Match Finished",
            MatchStatus.AET => "Extra time finish",
            MatchStatus.PEN => "Penalty finish",
            MatchStatus.BT => "Break Time(in Extra Time)",
            MatchStatus.SUSP => "Match Suspended",
            MatchStatus.INT => "Match Interrupted",
            MatchStatus.PST => "Match Postponed",
            MatchStatus.CANC => "Match Cancelled",
            MatchStatus.ABD => "Match Abandoned",
            MatchStatus.AWD => "Technical Loss",
            MatchStatus.WO => "WalkOver",
            MatchStatus.LIVE => "Live",
            _ => string.Empty
        };

    public static bool MatchIsFinished(this MatchStatus status)
        => status switch
        {
            MatchStatus.TBD => false,
            MatchStatus.NS => false,
            MatchStatus.FH => false,
            MatchStatus.HT => false,
            MatchStatus.SH => false,
            MatchStatus.ET => false,
            MatchStatus.P => false,
            MatchStatus.FT => true,
            MatchStatus.AET => true,
            MatchStatus.PEN => true,
            MatchStatus.BT => false,
            MatchStatus.SUSP => false,
            MatchStatus.INT => false,
            MatchStatus.PST => false,
            MatchStatus.CANC => true,
            MatchStatus.ABD => true,
            MatchStatus.AWD => true,
            MatchStatus.WO => true,
            MatchStatus.LIVE => false,
            _ => false
        };
}