using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineShop.Models
{
    public class Carrinho
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Produtos")]
        public List<Produto> Produtos { get; set; }
        [BsonElement("PrecoTotal")]
        public double PrecoTotal { get; set; }
    }
}
