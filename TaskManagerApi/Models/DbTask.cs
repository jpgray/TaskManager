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
}