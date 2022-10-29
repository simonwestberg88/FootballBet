using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Fixtures
    {
        [JsonProperty("events")]
        public bool Events { get; set; }

        [JsonProperty("lineups")]
        public bool Lineups { get; set; }

        [JsonProperty("statistics_fixtures")]
        public bool StatisticsFixtures { get; set; }

        [JsonProperty("statistics_players")]
        public bool StatisticsPlayers { get; set; }
    }

    


}
