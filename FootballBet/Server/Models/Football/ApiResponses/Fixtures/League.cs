using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class League
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("flag")]
        public object Flag { get; set; } //not returned in 2018

        [JsonProperty("season")]
        public int? Season { get; set; }

        [JsonProperty("round")]
        public string Round { get; set; }
    }
}
