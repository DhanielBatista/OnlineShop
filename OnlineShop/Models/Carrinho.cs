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

        public void AtualizarPrecoTotal()
        {
            PrecoTotal = CalcularPrecoTotal() ?? 0;
        }

        private double? CalcularPrecoTotal()
        {
            double? total = 0;
            foreach (var produto in Produtos)
            {
                total += produto.Preco;
            }
            return total;
        }
    }
}
