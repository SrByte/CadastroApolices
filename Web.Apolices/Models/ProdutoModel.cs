using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Apolices.Web.Models
{
	public class ProdutoModel
    {

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		[Required(ErrorMessage = "Campo obrigatório!")] 
        public string Nome { get; set; }

        [Range(0, 9999999, ErrorMessage = "Informe a quantidade adequada!")]
        public int Quantidade { get; set; }

        [Range(0.1, 9999999, ErrorMessage = "Informe o preço adequado!")]
        public double Preco { get; set; }

        public double ValorEstoqueTotal()
        {
            return Preco * Quantidade;
        }

    }
}
