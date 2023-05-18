namespace API_Csharp.Models
{
    public class TaskManagerDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TasksCollectionName { get; set; } = null!;

        public string CommentsCollectionName { get; set; } = null!;
    }
}
