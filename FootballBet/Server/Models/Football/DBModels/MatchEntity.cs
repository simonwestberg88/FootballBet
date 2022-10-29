using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBet.Server.Models.Football.DBModels
{
    public class MatchEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? HomeFulltimeGoals { get; set; }
        public int? AwayFulltimeGoals { get; set; }
        public int? HomeCurrentGoals { get; set; }
        public int? AwayCurrentGoals { get; set; }
        public int? HomePenaltyGoals { get; set; }
        public int? AwayPenaltyGoals { get; set; }
        public int? AwayTeamId { get; set; }
        public int? HomeTeamId { get; set; }
        public MatchStatus MatchStatus { get; set; }
        public string Round { get; set; }
        public int? Season { get; set; }
        public int LeagueId { get; set; }
        public virtual LeagueEntity League { get; set; }
        public virtual TeamEntity HomeTeam { get; set; }
        public virtual TeamEntity AwayTeam { get; set; }

    }
    public enum MatchStatus
    {
        TBD,
        NS,
        FH, //first half 1H from API
        HT,
        SH, //second half 2H from API
        ET,
        P,
        FT,
        AET,
        PEN,
        BT,
        SUSP,
        INT,
        PST,
        CANC,
        ABD,
        AWD,
        WO,
        LIVE,
    }
}
