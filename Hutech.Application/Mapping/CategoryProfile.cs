using AutoMapper;
using Hutech.Application.ViewModels;
using Hutech.Domain.Entities;

namespace Hutech.Application.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryVm>()
            .ReverseMap();
    }
}