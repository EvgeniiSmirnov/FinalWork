namespace FinalWork.Models.API;

public class GetProjectAnswer
{
    [JsonPropertyName("status")] public bool Status { get; set; }
    [JsonPropertyName("result")] public ResultData2? Result { get; set; }

    public class ResultData2
    {
        [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
        [JsonPropertyName("code")] public string Code { get; init; } = string.Empty;
        [JsonPropertyName("counts")] public Counts Counts { get; init; } = new();
    }

    public class Counts
    {
        [JsonPropertyName("cases")] public int Cases { get; set; }
        [JsonPropertyName("suites")] public int Suites { get; set; }
        [JsonPropertyName("milestones")] public int Milestones { get; set; }
        [JsonPropertyName("runs")] public RunsData Runs { get; init; } = new();
        [JsonPropertyName("defects")] public DefectsData Defects { get; init; } = new();

        public class RunsData
        {
            [JsonPropertyName("total")] public int Total { get; set; }
            [JsonPropertyName("active")] public int Active { get; set; }
        }
        public class DefectsData
        {
            [JsonPropertyName("total")] public int Total { get; set; }
            [JsonPropertyName("open")] public int Open { get; set; }
        }
    }
}