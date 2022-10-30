using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Score
    {
        [JsonProperty("halftime")]
        public MatchScore Halftime { get; set; }

        [JsonProperty("fulltime")]
        public MatchScore Fulltime { get; set; }

        [JsonProperty("extratime")]
        public MatchScore Extratime { get; set; }

        [JsonProperty("penalty")]
        public MatchScore Penalty { get; set; }
    }
}
