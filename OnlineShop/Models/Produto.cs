using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineShop.Models
{
    public class Produto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; } = null;
        [BsonElement("Preco")]
        public double Preco { get; set; }
        [BsonElement("Descrição")]
        public string Descricao { get; set; } = null;
        public bool ProdutoVendido { get; set; } = false;
    }
}
