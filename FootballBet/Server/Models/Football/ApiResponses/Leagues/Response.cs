using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Response
    {
        [JsonProperty("league")]
        public League League { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("seasons")]
        public List<Season> Seasons { get; set; }
    }

    


}
