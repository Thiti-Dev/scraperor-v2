using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace scraperor_v2.Dtos;

public class ScrapeResponseBodyDto
{

    [JsonPropertyName("success")]
    public bool? success { get; set; }
    [JsonPropertyName("contents")]
    public string[]? contents { get; set; }
}

