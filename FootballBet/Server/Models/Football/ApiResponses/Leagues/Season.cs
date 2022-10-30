using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Season
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("current")]
        public bool Current { get; set; }

        [JsonProperty("coverage")]
        public Coverage Coverage { get; set; }
    }
}
