using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OnlineShop.Models;
using OnlineShop.Models.Dtos.ProdutoDto;
using OnlineShop.Models.Enums;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(ProdutoService produtoService, IMapper mapper) =>
            (_produtoService, _mapper) = (produtoService, mapper);

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> BuscarProdutos([FromQuery] int? numeroPagina, [FromQuery] string? produtoNome, 
            [FromQuery] double? precoMaiorQue, [FromQuery] double? precoMenorQue, 
            [FromQuery] OrdenacaoEnum ordenacaoProduto) {

            var filtroLista = new List<FilterDefinition<Produto>>();
            SortDefinition<Produto> ordenacao = null;
            var produtoVendido = Builders<Produto>.Filter.Eq(c => c.ProdutoVendido, false);
            filtroLista.Add(produtoVendido);

            if (!numeroPagina.HasValue || numeroPagina <= 0)
            {
                numeroPagina = 1;
            }
            int skip = (int)(numeroPagina - 1) * 10;
            int limit = 10;

            if(produtoNome != null)
            {
                var filtro = Builders<Produto>.Filter.Eq(c => c.Nome, produtoNome);
                filtroLista.Add(filtro);
            }

            if (precoMaiorQue != null)
            {
                var filtro = Builders<Produto>.Filter.Gt(c => c.Preco, precoMaiorQue);
                filtroLista.Add(filtro);

            }

            if (precoMenorQue != null )
            {
                var filtro = Builders<Produto>.Filter.Lt(c => c.Preco, precoMenorQue);
                filtroLista.Add(filtro);
            }

            if (ordenacaoProduto == OrdenacaoEnum.decrescente)
            {
                ordenacao = Builders<Produto>.Sort.Descending(c => c.Preco);

            }
            else if (ordenacaoProduto == OrdenacaoEnum.crescente)
            {
                ordenacao = Builders<Produto>.Sort.Ascending(c => c.Preco);
                
            }


            if (filtroLista.Count == 0)
            {
                var produtos = await _produtoService.BuscarProdutosAsync(skip, limit, ordenacao:ordenacao);
                return produtos;
            }

            var filtros = Builders<Produto>.Filter.And(filtroLista);
            var produtosFiltrados = await _produtoService.BuscarProdutosAsync(skip, limit, filtros, ordenacao);

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
        public async Task<IActionResult> CriarProduto([FromBody] CriarProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            await _produtoService.CriarProdutoAsync(produto);

            return CreatedAtAction(nameof(BuscarProdutos), new {id = produto.Id}, produto);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> AtualizarProduto(string id,[FromBody] EditarProdutoDto editarProdutoDto)
        {
            var produto = await _produtoService.BuscarProdutosPorIdAsync(id);

            if(produto == null)
            {
                return NotFound();
            }

            _mapper.Map(editarProdutoDto, produto);

            await _produtoService.AtualizarProdutoAsync(id, produto);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeletarProduto(string id)
        {
            var produto = await _produtoService.BuscarProdutosPorIdAsync(id);

            if(produto == null)
            {
                return NotFound();
            }
            await _produtoService.DeletarProdutoAsync(id);
            return NoContent();
        } 

    }
}
