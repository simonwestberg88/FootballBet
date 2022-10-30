using Newtonsoft.Json;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Paging
    {
        [JsonProperty("current")]
        public int Current { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    


}
