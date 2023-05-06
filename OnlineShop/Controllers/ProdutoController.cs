using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService) =>
            _produtoService = produtoService;

        [HttpGet]
        public async Task<List<Produto>> BuscarProdutos([FromQuery] int? numeroPagina, [FromQuery] string? nome, [FromQuery] string? produto, [FromQuery] bool? produtoVendido) {

            var listaFiltros = new List<FilterDefinition<Produto>>();

            if(!numeroPagina.HasValue || numeroPagina <= 0)
            {
                numeroPagina = 1;
            }
            int skip = (int)(numeroPagina - 1) * 5;
            int limit = 5;

            var filtroLista = new List<FilterDefinition<Produto>>();

            if(produtoVendido != null)
            {
                if(produtoVendido == true)
                {
                    var filtro = Builders<Produto>.Filter.Eq(c => c.ProdutoVendido, produtoVendido);
                    filtroLista.Add(filtro);
                }
            }
            
            if(filtroLista.Count == 0)
            {
                var produtos = await _produtoService.BuscarProdutosAsync(skip, limit);
                return produtos;
            }

            var filtros = Builders<Produto>.Filter.And(filtroLista);
            var produtosFiltrados = await _produtoService.BuscarProdutosAsync(skip, limit, filtros);

            return produtosFiltrados;
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Produto>> BuscarProdutosPorId(string id)
        {
            var produto = await _produtoService.BuscarProdutosPorIdAsync(id);

            if(produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        [HttpPost]
        public async Task<IActionResult> CriarProduto(Produto novoProduto)
        {
            await _produtoService.CriarProdutoAsync(novoProduto);

            return CreatedAtAction(nameof(BuscarProdutos), new {id = novoProduto.Id}, novoProduto);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> AtualizarProduto(string id, Produto atualizaProduto)
        {
            var produto = await _produtoService.BuscarProdutosPorIdAsync(id);

            if(produto == null)
            {
                return NotFound();
            }

            atualizaProduto.Id = produto.Id;

            await _produtoService.AtualizarProdutoAsync(id, atualizaProduto);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeletarProduto(string id)
        {
            var produto = await (_produtoService.BuscarProdutosPorIdAsync(id));

            if(produto == null)
            {
                return NotFound();
            }
            await _produtoService.DeletarProdutoAsync(id);
            return NoContent();
        } 

    }
}
