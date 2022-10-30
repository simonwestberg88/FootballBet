using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Common
{
    public class Paging
    {
        [JsonPropertyName("current")]
        public int Current { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
