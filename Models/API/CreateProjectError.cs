namespace FinalWork.Models.API;

public class CreateProjectError
{
    [JsonPropertyName("status")] public bool Status { get; set; }
    [JsonPropertyName("errorMessage")] public string ErrorMessage { get; set; }
    [JsonPropertyName("errorFields")] public ErrorData[] ErrorFields { get; set; }

    public class ErrorData
    {
        [JsonPropertyName("field")] public string? Field { get; set; }
        [JsonPropertyName("error")] public string? Error { get; set; }
    }
}