using API_Csharp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API_Csharp.Services
{
    public class CommentsService
    {
        private readonly IMongoCollection<Comment> _commentsCollection;

        public CommentsService(
            IOptions<TaskManagerDatabaseSettings> taskManagerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                taskManagerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                taskManagerDatabaseSettings.Value.DatabaseName);

            _commentsCollection = mongoDatabase.GetCollection<Comment>(
                taskManagerDatabaseSettings.Value.CommentsCollectionName);
        }

        public async Task<List<Comment>> GetAsync() =>
            await _commentsCollection.Find(_ => true).ToListAsync();

        public async Task<Comment?> GetAsync(string id) =>
            await _commentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Comment>> GetForTaskAsync(string taskId) =>
            await _commentsCollection.Find(x => x.Task == taskId).ToListAsync();

        public async System.Threading.Tasks.Task CreateAsync(Comment newComment) =>
            await _commentsCollection.InsertOneAsync(newComment);

        public async System.Threading.Tasks.Task UpdateAsync(string id, Comment updatedComment) =>
            await _commentsCollection.ReplaceOneAsync(x => x.Id == id, updatedComment);

        public async System.Threading.Tasks.Task RemoveAsync(string id) =>
            await _commentsCollection.DeleteOneAsync(x => x.Id == id);

        public async System.Threading.Tasks.Task RemoveForTaskAsync(string id) =>
            await _commentsCollection.DeleteManyAsync(x => x.Task == id);
    }
}

