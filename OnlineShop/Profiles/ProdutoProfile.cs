using AutoMapper;
using OnlineShop.Models;
using OnlineShop.Models.Dtos.ProdutoDto;

namespace OnlineShop.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CriarProdutoDto, Produto>();
            CreateMap<EditarProdutoDto, Produto>();
        }
    }
}
