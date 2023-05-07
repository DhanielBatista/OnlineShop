using MongoDB.Bson;

namespace OnlineShop.Models
{
    public class Carrinho
    {
        public string Id { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
