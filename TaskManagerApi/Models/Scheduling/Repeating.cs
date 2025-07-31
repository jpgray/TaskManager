namespace TaskManagerApi.Models.Scheduling;

public class Repeating : Schedule
{
    public RepeatPattern repeatPattern { get; set; }
}