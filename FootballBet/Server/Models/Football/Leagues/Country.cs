using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public object Code { get; set; }

        [JsonProperty("flag")]
        public object Flag { get; set; }
    }
}
