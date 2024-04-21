namespace FinalWork.Models.API;

public class CreateProjectAnswer
{
    [JsonPropertyName("status")] public bool Status { get; set; }
    [JsonPropertyName("result")] public ResultData? Result { get; set; }

    public class ResultData
    {
        [JsonPropertyName("code")] public string? Code { get; set; }
    }
}