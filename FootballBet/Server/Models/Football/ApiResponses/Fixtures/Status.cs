﻿using System.Text.Json.Serialization;

namespace FootballBet.Server.Models.Football.ApiResponses.Fixtures
{
    public class Status
    {
        [JsonPropertyName("long")]
        public string Long { get; set; }

        [JsonPropertyName("short")]
        public string Short { get; set; }

        [JsonPropertyName("elapsed")]
        public int? Elapsed { get; set; }
    }
}
