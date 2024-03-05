using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ThirdParty.Json.LitJson;

namespace WebMongoAPI.Models;

public class Seguro
{

	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }
	public string Seguradora { get; set; }
    public string NumeroApolice { get; set; }
    //public List<Cobertura> Coberturas { get; set; }
    //public bool BeneficiariosIndicados { get; set; }
    //public List<Beneficiario> Beneficiarios { get; set; }
    //public string AnexarApolice { get; set; }
    public string DataAquisicaoApolice { get; set; }
    public string ValorPremio { get; set; }
    public string FrequenciaPagamento { get; set; }
    public string Satisfacao { get; set; }
}
