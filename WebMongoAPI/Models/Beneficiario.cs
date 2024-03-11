 using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebMongoAPI.Models;

 public class Beneficiario
    {
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }
	public string Nome { get; set; }
    public string PercentualBeneficio { get; set; }
    }