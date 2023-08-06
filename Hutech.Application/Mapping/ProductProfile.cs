using AutoMapper;
using Hutech.Application.ViewModels;
using Hutech.Domain.Entities;

namespace Hutech.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ReverseMap();

        CreateMap<Product, ProductRequest>()
            .ReverseMap();
    }
}