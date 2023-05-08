using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineShop.Models;
using OnlineShop.Models.DatabaseSettings;

namespace OnlineShop.Services
{
    public class VendaService
    {
        private readonly IMongoCollection<Venda> _vendaCollection;

        public VendaService(IOptions<OnlineShopDatabaseSettings> onlineShopDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                onlineShopDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                onlineShopDatabaseSettings.Value.DatabaseName);
            _vendaCollection = mongoDatabase.GetCollection<Venda>(
                onlineShopDatabaseSettings.Value.VendaCollectionName);
        }

        public async Task<List<Venda>> BuscarVendaAsync() =>
         await _vendaCollection.Find(_ => true).ToListAsync();
        public async Task<Venda> BuscarVendaPorIdAsync(string id)
        {
            return await _vendaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task CriarVendaAsync(Venda venda) =>
           await _vendaCollection.InsertOneAsync(venda);

        public async Task DeletarVendaAsync(string id) =>
            await _vendaCollection.DeleteOneAsync(x => x.Id == id);
    }
}
