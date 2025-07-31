namespace TaskManagerApi.Models;

using Models.Scheduling;
using TaskManagerApi.Support.Exceptions;

public class TodoTask
{
    public string? id { get; init; }
    public required int userId { get; init; }
    public required string title { get; set; }
    public string? details { get; set; }
    public Schedule SchedulingDetails { get; private set; } = new Unscheduled();
    void ConvertSchedulingDetails(DbTask task)
    {
        if (!task.scheduled)
        {
            this.SchedulingDetails = new Unscheduled();
            return;
        }
        if (task.dueDate is not null)
        {
            if ((task.repeatPattern ?? RepeatPattern.None) is not RepeatPattern.None)
            {
                this.SchedulingDetails = new Repeating();
            }
            else
            {
                this.SchedulingDetails = new OneOff();
            }
            return;
        }
        throw new TaskFormatInvalidException(task);
    }
    public static TodoTask fromDbTask(DbTask dbTask)
    {
        var task = new TodoTask()
        {
            id = dbTask.id,
            userId = dbTask.userId,
            title = dbTask.title,
            details = dbTask.details
        };

        task.ConvertSchedulingDetails(dbTask);
        return task;
    }
}