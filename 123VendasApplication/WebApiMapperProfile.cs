using _123Vendas.Domain.Models;
using _123VendasApplication.Dto;
using AutoMapper;

namespace _123VendasApplication
{
    public class WebApiMapperProfile : Profile
    {
        public WebApiMapperProfile()
        {
        
            CreateMap<Client, ClientDto>()
                .ReverseMap();

            CreateMap<BranchStore, BranchStoreDto>()
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.statusItem, opt => opt.MapFrom(src => src.statusItem))
                .ReverseMap();

            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.BranchStore, opt => opt.MapFrom(src => src.BranchStore))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

            CreateMap<Sale, SalePutDto>()
            .IncludeBase<Sale, SaleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
            .ReverseMap();
        }
    }
}
