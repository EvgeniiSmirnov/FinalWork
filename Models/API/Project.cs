namespace FinalWork.Models.API;

public class Project
{
    [JsonPropertyName("title")] public required string Title { get; set; }
    [JsonPropertyName("code")] public required string Code { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
}