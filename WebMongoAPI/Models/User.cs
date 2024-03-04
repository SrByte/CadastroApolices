using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebMongoAPI.Models;

public class User
{
    public ObjectId Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }

    public Group? Group { get; set; } = null;
}

public class Group
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}