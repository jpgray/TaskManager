namespace TaskManagerApi.Models;

using Models.Scheduling;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DbTask
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set; }
    public required int userId { get; init; }
    public required string title { get; set; }
    public string? details { get; set; }
    public bool scheduled { get; set; } = false;
    public DateTime? dueDate { get; set; }
    public RepeatPattern? repeatPattern { get; set; }

    public static DbTask FromTodoTask(TodoTask todo)
    {
        DbTask _task = new DbTask
        {
            id = todo.id,
            userId = todo.userId,
            title = todo.title,
            details = todo.details,
            scheduled = todo.schedulingDetails == null ? false : todo.schedulingDetails.GetType() != typeof(Unscheduled)
        };
        if (todo.schedulingDetails == null)
        {
            _task.scheduled = false;
        }
        else
        {
            switch (todo.schedulingDetails.GetType().Name)
            {
                case "OneOff":
                    _task.scheduled = true;
                    OneOff oo = (OneOff)todo.schedulingDetails;
                    _task.dueDate = oo.dueDate;
                    break;
                case "Repeating":
                    _task.scheduled = true;
                    Repeating rp = (Repeating)todo.schedulingDetails;
                    _task.dueDate = rp.nextDueDate;
                    _task.repeatPattern = rp.repeatPattern;
                    break;
                default:
                    _task.scheduled = false;
                    break;
            }
        }
        return _task;

    }
}