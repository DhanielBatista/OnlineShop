namespace OnlineShop.Models.DatabaseSettings
{
    public class OnlineShopDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ProdutoCollectionName { get; set; }
    }
}
