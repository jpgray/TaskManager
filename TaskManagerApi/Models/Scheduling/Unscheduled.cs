namespace TaskManagerApi.Models.Scheduling;

public class Unscheduled : Schedule
{
    public Unscheduled()
    {
        scheduleState = ScheduleState.Unscheduled;
    }
    public override void SetDueDate(DateTime date)
    {
        return;
    }
}