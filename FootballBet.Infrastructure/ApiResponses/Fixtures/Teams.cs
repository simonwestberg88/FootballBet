﻿using System.Text.Json.Serialization;

namespace FootballBet.Infrastructure.ApiResponses.Fixtures;

public class Teams
{
    [JsonPropertyName("home")]
    public Team Home { get; set; }

    [JsonPropertyName("away")]
    public Team Away { get; set; }
}