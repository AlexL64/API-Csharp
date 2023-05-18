using API_Csharp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API_Csharp.Services
{
    public class TasksService
    {
        private readonly IMongoCollection<Models.Task> _tasksCollection;

        public TasksService(
            IOptions<TaskManagerDatabaseSettings> taskManagerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                taskManagerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                taskManagerDatabaseSettings.Value.DatabaseName);

            _tasksCollection = mongoDatabase.GetCollection<Models.Task>(
                taskManagerDatabaseSettings.Value.TasksCollectionName);
        }

        public async Task<List<Models.Task>> GetAsync() =>
            await _tasksCollection.Find(_ => true).ToListAsync();

        public async Task<Models.Task?> GetAsync(string id) =>
            await _tasksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async System.Threading.Tasks.Task CreateAsync(Models.Task newTask) =>
            await _tasksCollection.InsertOneAsync(newTask);

        public async System.Threading.Tasks.Task UpdateAsync(string id, Models.Task updatedTask) =>
            await _tasksCollection.ReplaceOneAsync(x => x.Id == id, updatedTask);

        public async System.Threading.Tasks.Task RemoveAsync(string id) =>
            await _tasksCollection.DeleteOneAsync(x => x.Id == id);
    }
}
