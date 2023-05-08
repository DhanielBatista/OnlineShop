using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Models.Dtos.VendaDto;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            // Atualiza o ProdutoVendido de cada produto no carrinho para true
            foreach (var produto in carrinho.Produtos)
            {
                produto.ProdutoVendido = true;
                await _produtoService.AtualizarProdutoAsync(produto.Id, produto);
            }
            venda.AtualizarValorTotal();
            await _vendaService.CriarVendaAsync(venda);

            return Ok(venda);
        }

    }
}
