using Microsoft.AspNetCore.Mvc;
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
        public async Task<List<Produto>> BuscarProdutos() =>
            await _produtoService.BuscarProdutosAsync();

        [HttpGet("{id:lengh(24)")]
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

    }
}
