using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace WebMongoAPI.Models;

public class Cobertura
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }
	public string Descricao { get; set; }
    public string CapitalSegurado { get; set; }
}