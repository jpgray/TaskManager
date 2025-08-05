namespace TaskManagerApi.Models;

using System.Text.Json.Serialization;
using Models.Scheduling;
using TaskManagerApi.Models.Converters;
using TaskManagerApi.Support.Exceptions;

public class TodoTask
{
    public string? id { get; set; }
    public required int userId { get; init; }
    public required string title { get; set; }
    public string? details { get; set; }
    [JsonConverter(typeof(ScheduleConverter))]
    public Schedule? schedulingDetails { get; private set; }
    public static Schedule ConvertSchedulingDetails(DbTask task)
    {
        if (!task.scheduled)
        {
            return new Unscheduled();
        }
        if (task.dueDate is not null)
        {
            if ((task.repeatPattern ?? RepeatPattern.None) is not RepeatPattern.None)
            {
                return new Repeating()
                {
                    repeatPattern = task.repeatPattern!.Value,
                    nextDueDate = task.dueDate!.Value
                };
            }
            else
            {
                return new OneOff()
                {
                    dueDate = task.dueDate!.Value
                };
            }
        }
        throw new TaskFormatInvalidException(task);
    }
    public static TodoTask FromDbTask(DbTask dbTask)
    {
        return new TodoTask()
        {
            id = dbTask.id,
            userId = dbTask.userId,
            title = dbTask.title,
            details = dbTask.details,
            schedulingDetails = ConvertSchedulingDetails(dbTask)
        };
    }

    public void SetDueDate(DateTime date)
    {
        this.schedulingDetails!.SetDueDate(date);
    }
}