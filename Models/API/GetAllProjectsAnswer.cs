namespace FinalWork.Models.API;

public class GetAllProjectsAnswer
{
    [JsonPropertyName("status")] public bool Status { get; set; }
    [JsonPropertyName("result")] public ResultData3? Result { get; set; }

    public class ResultData3
    {
        [JsonPropertyName("total")] public int Total { get; set; }
        [JsonPropertyName("filtered")] public int Filtered { get; init; }
        [JsonPropertyName("count")] public int Count { get; init; }
        [JsonPropertyName("entities")] public EntitiesData[] Entities { get; set; }
    }
    public class EntitiesData { }
}