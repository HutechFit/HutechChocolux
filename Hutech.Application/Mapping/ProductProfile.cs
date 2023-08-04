using AutoMapper;
using Hutech.Application.Models;
using Hutech.Domain.Entities;

namespace Hutech.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name))
            .ReverseMap();

        CreateMap<Product, ProductRequest>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ReverseMap();
    }
}