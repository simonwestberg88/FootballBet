using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Season
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("start")]
        public string Start { get; set; }

        [JsonPropertyName("end")]
        public string End { get; set; }

        [JsonPropertyName("current")]
        public bool Current { get; set; }

        [JsonPropertyName("coverage")]
        public Coverage Coverage { get; set; }
    }
}
