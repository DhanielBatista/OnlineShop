using AutoMapper;
using OnlineShop.Models;
using OnlineShop.Models.Dtos.CarrinhoDto;

namespace OnlineShop.Profiles
{
    public class CarrinhoProfile : Profile
    {
        public CarrinhoProfile()
        {
            CreateMap<CriarCarrinhoDto, Carrinho>();
            CreateMap<EditarCarrinhoDto, Carrinho>().ForMember(dest => dest.Produtos, opt => opt.MapFrom(src =>
        src.Produtos.Select(p => new Produto { Nome = p }).ToList()));
        }
    }
}
