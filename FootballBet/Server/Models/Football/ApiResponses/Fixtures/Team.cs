using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Team
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("winner")]
        public bool? Winner { get; set; }
    }
}
