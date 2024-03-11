using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Apolices.Web.Models
{
	public class BeneficiarioModel
    {

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		public string Nome { get; set; }
        public string PercentualBeneficio { get; set; }
    }
}
