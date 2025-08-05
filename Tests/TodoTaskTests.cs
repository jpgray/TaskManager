namespace TaskManagerApi.Models;

using FluentAssertions;
using TaskManagerApi.Models.Scheduling;

public class TodoTaskTests
{
    [Test]
    public void ConvertSchedulingDetails_Unscheduled_Nulls()
    {
        DbTask dbTask = new DbTask()
        {
            userId = 3,
            title = "title"
        };
        var result = TodoTask.ConvertSchedulingDetails(dbTask);
        result.Should().BeOfType<Unscheduled>("No scheduling details provided should result in Unscheduled object");
    }

    [Test]
    public void ConvertSchedulingDetails_OneOff_NullRepeating()
    {
        DbTask dbTask = new DbTask()
        {
            userId = 3,
            title = "title",
            scheduled = true,
            dueDate = DateTime.Now
        };
        var result = TodoTask.ConvertSchedulingDetails(dbTask);
        result.Should().BeOfType<OneOff>("scheduled true and duedate provided should result in OneOff type if no repeat pattern provided");
    }

    [Test]
    public void ConvertSchedulingDetails_OneOff_RepeatingNone()
    {
        DbTask dbTask = new DbTask()
        {
            userId = 3,
            title = "title",
            scheduled = true,
            dueDate = DateTime.Now,
            repeatPattern = RepeatPattern.None
        };
        var result = TodoTask.ConvertSchedulingDetails(dbTask);
        result.Should().BeOfType<OneOff>("scheduled true and duedate provided should result in OneOff type if repeat pattern 'None' provided");
    }

    [Test]
    public void ConvertSchedulingDetails_Repeating_RepeatingDaily()
    {
        DbTask dbTask = new DbTask()
        {
            userId = 3,
            title = "title",
            scheduled = true,
            dueDate = DateTime.Now,
            repeatPattern = RepeatPattern.Daily
        };
        var result = TodoTask.ConvertSchedulingDetails(dbTask);
        result.Should().BeOfType<Repeating>("scheduled true and duedate provided should result in Repeating type if repeat pattern 'Daily' provided");
    }
}
