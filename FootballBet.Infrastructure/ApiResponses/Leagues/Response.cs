using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Response
    {
        [JsonPropertyName("league")]
        public League League { get; set; }

        [JsonPropertyName("country")]
        public Country Country { get; set; }

        [JsonPropertyName("seasons")]
        public List<Season> Seasons { get; set; }
    }

    


}
