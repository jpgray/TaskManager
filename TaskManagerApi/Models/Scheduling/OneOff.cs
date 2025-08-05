namespace TaskManagerApi.Models.Scheduling;

public class OneOff : Schedule
{
    public OneOff()
    {
        scheduleState = ScheduleState.OneOff;
    }
    public DateTime dueDate { get; set; }
    public override void SetDueDate(DateTime date)
    {
        this.dueDate = date;
    }
}