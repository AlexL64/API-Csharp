using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_Csharp.Models;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? Task { get; set; }

    public string Text { get; set; } = null!;

    public DateTime Created { get; set; }
}
