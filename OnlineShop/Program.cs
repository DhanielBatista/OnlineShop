using OnlineShop.Models.DatabaseSettings;
using OnlineShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OnlineShopDatabaseSettings>(builder.Configuration.GetSection("OnlineShopDatabase"));
builder.Services.AddSingleton<ProdutoService>();
builder.Services.AddSingleton<CarrinhoService>();
builder.Services.AddSingleton<VendaService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
