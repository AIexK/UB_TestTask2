using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using TestTask.Domain;

namespace TestTask.Application.Common.Utilites.JsonConverters;

internal class JsonDateTimeOffsetFormatConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTimeOffset.ParseExact(reader.GetString().Replace(' ', '+'), Constants.DATE_TIME_OFFSET_FROM_GET_REQUEST_FORMAT, CultureInfo.InvariantCulture);

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
     => writer.WriteStringValue(value.ToString(Constants.DATE_TIME_OFFSET_FROM_GET_REQUEST_FORMAT, CultureInfo.InvariantCulture));
}
