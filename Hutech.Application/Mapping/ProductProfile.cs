using AutoMapper;
using Hutech.Application.ViewModels;
using Hutech.Domain.Entities;

namespace Hutech.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductVm>()
            .ReverseMap();
    }
}