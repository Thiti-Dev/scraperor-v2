using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace scraperor_v2.Dtos;

public class ScrapeBodyPointerLookForDto
{
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }
    [JsonPropertyName("has_classes")]
    public string[]? HasClasses { get; set; }
    [JsonPropertyName("then_look_for")]
    public ScrapeBodyPointerLookForDto? ThenLookFor { get; set; }


}

public class ScrapeBodyPointerDto
{
    [Required(),JsonPropertyName("look_for")]
    public ScrapeBodyPointerLookForDto? LookFor { get; set; }


}

public class ScrapeBodyDto
	{
    [Required(),JsonPropertyName("pointer")]
    public ScrapeBodyPointerDto? Pointer { get; set; }
    [Required(), JsonPropertyName("website")]
    public string? Website { get; set; }

}

