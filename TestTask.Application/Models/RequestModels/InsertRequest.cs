using System.Text.Json.Serialization;
using TestTask.Application.Common.Utilites.JsonConverters;

namespace TestTask.Application.Models.RequestModels;

public record InsertRequest
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("operationDate")]
    [JsonConverter(typeof(JsonDateTimeOffsetFormatConverter))]
    public DateTimeOffset OperationDate { get; init; }

    [JsonPropertyName("amount")]
    public float Amount { get; init; }
}
