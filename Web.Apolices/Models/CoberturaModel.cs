using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Apolices.Web.Models
{
	public class CoberturaModel
    {

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
		public string Descricao { get; set; }
        public decimal CapitalSegurado { get; set; }
    }
}
