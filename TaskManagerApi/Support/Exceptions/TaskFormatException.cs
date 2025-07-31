using TaskManagerApi.Models;

namespace TaskManagerApi.Support.Exceptions;

public class TaskFormatInvalidException : Exception
{
    public DbTask? dbTask;
    public TaskFormatInvalidException(DbTask _task)
    {
        dbTask = _task;
    }
}