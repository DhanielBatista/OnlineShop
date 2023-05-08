using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Models.Dtos.CarrinhoDto;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoService _carrinhoService;
        private readonly ProdutoService _produtoService;
        private readonly IMapper _mapper;

        public CarrinhoController(CarrinhoService carrinhoService, ProdutoService produtoService, IMapper mapper) =>
            (_carrinhoService, _produtoService, _mapper) = (carrinhoService, produtoService, mapper);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Carrinho>> BuscarCarrinhoId(string id)
        {
           
            var carrinho = await _carrinhoService.BuscarCarrinhoPorIdAsync(id);

            if(carrinho == null)
            {
                return NotFound();
            }
           
            return carrinho;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCarrinho([FromBody] CriarCarrinhoDto carrinhoDto)
        {
            var produtos = new List<Produto>();
            foreach (var produtoId in carrinhoDto.Produtos)
            {
                var produto = await _produtoService.BuscarProdutosPorIdAsync(produtoId);
                if (produto == null)
                {
                    return BadRequest($"Produto {produtoId} não encontrado.");
                }
                if (produto.ProdutoVendido)
                {
                    return BadRequest($"Produto {produtoId} já foi vendido.");
                }
                produtos.Add(produto);
            }

            var carrinho = new Carrinho
            {
                Produtos = produtos
            };
            carrinho.AtualizarPrecoTotal();
            await _carrinhoService.CriarCarrinhoAsync(carrinho);

            return Ok(carrinho);
        }

        [HttpPut]
        public async Task<IActionResult>AtualizarCarrinho(string id,[FromBody] EditarCarrinhoDto carrinhoDto)
        {
            var carrinho = await _carrinhoService.BuscarCarrinhoPorIdAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }
            var produtosRemover = carrinho.Produtos.Where(p => !carrinhoDto.Produtos.Contains(p.Id)).ToList();
            if (produtosRemover.Any())
            {
                carrinho.Produtos.RemoveAll(p => produtosRemover.Contains(p));
            }

            // Adiciona os novos produtos ao carrinho
            var produtosAdicionar = carrinhoDto.Produtos.Where(p => !carrinho.Produtos.Any(cp => cp.Id == p)).ToList();
            if (produtosAdicionar.Any())
            {
                foreach (var produtoId in produtosAdicionar)
                {
                    var produto = await _produtoService.BuscarProdutosPorIdAsync(produtoId);
                    if (produto != null)
                    {
                        carrinho.Produtos.Add(produto);
                    }
                }
            }

            await _carrinhoService.AtualizarCarrinhoAsync(id, carrinho);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletaCarrinho(string id)
        {
            var carrinho = await _carrinhoService.BuscarCarrinhoPorIdAsync(id);
            if(carrinho == null)
            {
                return NotFound();
            }
            await _carrinhoService.DeletarCarrinhoAsync(id);
            return NoContent();
        }

    }
}
