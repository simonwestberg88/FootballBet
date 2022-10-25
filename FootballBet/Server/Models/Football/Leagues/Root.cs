using FootballBet.Server.Models.Football.Common;
using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.Leagues
{
    public class Root
    {
        [JsonPropertyName("get")]
        public string Get { get; set; }

        [JsonPropertyName("parameters")]
        public List<object> Parameters { get; set; }

        [JsonPropertyName("errors")]
        public List<object> Errors { get; set; }

        [JsonPropertyName("results")]
        public int Results { get; set; }

        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }

        [JsonPropertyName("response")]
        public List<LeaguesResponse> Response { get; set; }
    }
}
