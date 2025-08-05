using System.Text.Json.Serialization;
using JsonSubTypes;
using TaskManagerApi.Models.Converters;

namespace TaskManagerApi.Models.Scheduling;

[JsonConverter(typeof(ScheduleConverter))]
public abstract class Schedule
{
    public abstract void SetDueDate(DateTime date);
    protected ScheduleState scheduleState { get; set; }
}