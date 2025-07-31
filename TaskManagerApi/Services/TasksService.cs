namespace TaskManagerApi.Services;

using MongoDB.Driver;
using TaskManagerApi.Models;

public class TasksService
{
    private string databaseName = "TaskManager";
    private string collectionName = "Tasks";
    private readonly IMongoCollection<DbTask> _tasksCollection;

    public TasksService()
    {
        var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));

        var mongoDatabase = mongoClient.GetDatabase(
            databaseName);

        _tasksCollection = mongoDatabase.GetCollection<DbTask>(
            collectionName);
    }
    public async Task<List<DbTask>> GetAsync() =>
            await _tasksCollection.Find(_ => true).ToListAsync();

    public async Task<DbTask?> GetAsync(string id) =>
        await _tasksCollection.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(DbTask newBook) =>
        await _tasksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, DbTask updatedBook) =>
        await _tasksCollection.ReplaceOneAsync(x => x.id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _tasksCollection.DeleteOneAsync(x => x.id == id);
}