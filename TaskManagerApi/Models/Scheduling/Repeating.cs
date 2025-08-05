namespace TaskManagerApi.Models.Scheduling;

public class Repeating : Schedule
{
    public Repeating()
    {
        scheduleState = ScheduleState.Repeating;
    }
    public RepeatPattern repeatPattern { get; set; }
    public DateTime nextDueDate { get; set; }
    public override void SetDueDate(DateTime date)
    {
        this.nextDueDate = date;
    }
}