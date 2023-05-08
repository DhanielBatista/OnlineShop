using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineShop.Models;
using OnlineShop.Models.DatabaseSettings;

namespace OnlineShop.Services
{
    public class CarrinhoService
    {
        private readonly IMongoCollection<Carrinho> _carrinhoCollection;


        public CarrinhoService(IOptions<OnlineShopDatabaseSettings> onlineShopDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                onlineShopDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                onlineShopDatabaseSettings.Value.DatabaseName);
            _carrinhoCollection = mongoDatabase.GetCollection<Carrinho>(
                onlineShopDatabaseSettings.Value.CarrinhoCollectionName);
        }

        public async Task<List<Carrinho>> BuscarCarrinhoAsync(int skip, int limit, FilterDefinition<Carrinho> filtro = null)
        {

            var carrinhos = await _carrinhoCollection.Find(filtro ?? Builders<Carrinho>.Filter.Empty)
                            .Skip(skip)
                            .Limit(limit)
                            .ToListAsync();
            return carrinhos;

        }
        public async Task<Carrinho> BuscarCarrinhoPorIdAsync(string id)
        {
            return await _carrinhoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task CriarCarrinhoAsync(Carrinho carrinho) =>
            await _carrinhoCollection.InsertOneAsync(carrinho);

        public async Task AtualizarCarrinhoAsync(string id, Carrinho carrinho) =>
            await _carrinhoCollection.ReplaceOneAsync(x => x.Id == id, carrinho);

        public async Task DeletarCarrinhoAsync(string id) =>
            await _carrinhoCollection.DeleteOneAsync(x => x.Id == id);


    }
}
