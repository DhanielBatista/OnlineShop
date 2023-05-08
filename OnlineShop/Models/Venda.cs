using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineShop.Models
{
    public class Venda
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("DataVenda")]
        public DateTime DataVenda { get; set; } = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        [BsonElement("Carrinho")]
        public List<Carrinho> Carrinho { get; set; }
        [BsonElement("CupomDesconto")]
        public double CupomDesconto { get; set; }
        [BsonElement("ValorTotal")]
        public double ValorFinal { get; set; }
    

        public void AtualizarValorTotal()
        {
            double precoTotal = Carrinho.Sum(item => item.PrecoTotal);
            ValorFinal = precoTotal - CupomDesconto;
        }
    }
}
