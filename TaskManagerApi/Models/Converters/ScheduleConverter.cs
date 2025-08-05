using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManagerApi.Models.Scheduling;

namespace TaskManagerApi.Models.Converters;

public class ScheduleConverter : JsonConverter<Schedule>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Schedule).IsAssignableFrom(typeToConvert);
    }

    public override void Write(Utf8JsonWriter writer, Schedule value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }

    public override Schedule? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            ScheduleState state = (ScheduleState)jsonDoc.RootElement.GetProperty("scheduleState").GetInt16();
            switch (state)
            {
                case ScheduleState.OneOff:
                    return new OneOff()
                    {
                        dueDate = jsonDoc.RootElement.GetProperty("dueDate").GetDateTime(),
                    };
                case ScheduleState.Repeating:
                    return new Repeating()
                    {
                        nextDueDate = jsonDoc.RootElement.GetProperty("dueDate").GetDateTime(),
                    };
                default:
                    return new Unscheduled();

            }
        }
    }
}