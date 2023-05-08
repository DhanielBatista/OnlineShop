using AutoMapper;
using OnlineShop.Models;
using OnlineShop.Models.Dtos.VendaDto;

namespace OnlineShop.Profiles
{
    public class VendaProfile : Profile
    {
        public VendaProfile()
        {
            CreateMap<CriarVendaDto,Venda>();
            CreateMap<BuscarVendaDto,Venda>();
        }
    }
}
