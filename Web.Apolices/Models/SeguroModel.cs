using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Apolices.Web.Models
{
	public class SeguroModel
    {

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
		public string Seguradora { get; set; }
        public string NumeroApolice { get; set; }
        //public List<CoberturaModel> Coberturas { get; set; }
        //public List<BeneficiarioModel> Beneficiarios { get; set; }
        //public string AnexarApolice { get; set; }
        public string DataAquisicaoApolice { get; set; }
        public string ValorPremio { get; set; }
        public string FrequenciaPagamento { get; set; }
        public string Satisfacao { get; set; }
    }
}
