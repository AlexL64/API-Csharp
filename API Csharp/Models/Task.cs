using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_Csharp.Models;

public class Task
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Created { get; set; }

    public string Status { get; set; } = null!;
}
