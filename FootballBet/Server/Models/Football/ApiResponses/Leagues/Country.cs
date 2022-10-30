using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("flag")]
        public string? Flag { get; set; }
    }
}
