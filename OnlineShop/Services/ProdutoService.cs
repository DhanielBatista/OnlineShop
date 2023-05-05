using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineShop.Models;
using OnlineShop.Models.DatabaseSettings;

namespace OnlineShop.Services
{
    public class ProdutoService
    {
        private readonly IMongoCollection<Produto> _produtoCollection;

        public ProdutoService(
            IOptions<OnlineShopDatabaseSettings> onlineShopDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                onlineShopDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                onlineShopDatabaseSettings.Value.DatabaseName);
            _produtoCollection = mongoDatabase.GetCollection<Produto>(
                onlineShopDatabaseSettings.Value.ProdutoCollectionName);
        }
        
        public async Task<List<Produto>> BuscarProdutosAsync() =>
            await _produtoCollection.Find(_ => true).ToListAsync();
        
        public async Task<Produto?> BuscarProdutosPorIdAsync(string id) =>
            await _produtoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CriarProdutoAsync(Produto produto) => 
            await _produtoCollection.InsertOneAsync(produto);

        public async Task AtualizarProdutoAsync(string id, Produto produto) =>
            await _produtoCollection.ReplaceOneAsync(x => x.Id == id, produto);

        public async Task DeletarProdutoAsync(string id) =>
            await _produtoCollection.DeleteOneAsync(x => x.Id == id);


    }
}
