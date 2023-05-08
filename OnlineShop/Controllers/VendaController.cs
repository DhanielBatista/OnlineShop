using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OnlineShop.Models;
using OnlineShop.Models.Dtos.VendaDto;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly VendaService _vendaService;
        private readonly CarrinhoService _carrinhoService;
        private readonly ProdutoService _produtoService;

        public VendaController(IMapper mapper, VendaService vendaService, CarrinhoService carrinhoService, ProdutoService produtoService) =>
            (_mapper, _vendaService, _carrinhoService, _produtoService) = (mapper, vendaService, carrinhoService, produtoService);

        [HttpGet]
        public async Task<List<Venda>> BuscarVendas()
        {
            return await _vendaService.BuscarVendaAsync();
        }
        [HttpPost]
        public async Task<IActionResult> CriarVenda([FromBody] CriarVendaDto vendaDto)
        {
            var carrinho = await _carrinhoService.BuscarCarrinhoPorIdAsync(vendaDto.CarrinhoId);
            if (carrinho == null)
            {
                return BadRequest($"Carrinho {vendaDto.CarrinhoId} não encontrado.");
            }

            var venda = new Venda
            {
                Carrinho = new List<Carrinho> { carrinho },
                DataVenda = DateTime.Now,
                CupomDesconto = vendaDto.CupomDesconto
            };

            foreach (var produto in carrinho.Produtos)
            {
                produto.ProdutoVendido = true;
                await _produtoService.AtualizarProdutoAsync(produto.Id, produto);
            }
            venda.AtualizarValorTotal();
            await _vendaService.CriarVendaAsync(venda);

            return Ok(venda);
        }
        [HttpPost]
        [Route("/ProdutosVendidos")]
        public async Task<IActionResult> BuscarVendasPorPeriodo([FromBody] BuscarVendaDto vendaDto)
        {
            var filter = Builders<Venda>.Filter.And(
          Builders<Venda>.Filter.Gte(v => v.DataVenda, vendaDto.DataInicio),
          Builders<Venda>.Filter.Lte(v => v.DataVenda, vendaDto.DataFim));

            var vendas = await _vendaService.BuscarVendaAsync();

            if (vendas == null || !vendas.Any())
            {
                return NotFound();
            }

            return Ok(vendas);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaVenda(string id)
        {
            var venda = await _vendaService.BuscarVendaPorIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            foreach (var carrinho in venda.Carrinho)
            {
                foreach (var produto in carrinho.Produtos)
                {
                    produto.ProdutoVendido = false;
                    await _produtoService.AtualizarProdutoAsync(produto.Id, produto);
                }
            }

            await _vendaService.DeletarVendaAsync(id);

            return NoContent();
        }


    }
}
