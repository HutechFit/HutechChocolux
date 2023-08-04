using AutoMapper;
using Hutech.Application.Models;
using Hutech.Domain.Entities;

namespace Hutech.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductVm>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ReverseMap();
    }
}