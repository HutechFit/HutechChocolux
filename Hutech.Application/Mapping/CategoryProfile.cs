using AutoMapper;
using Hutech.Application.Models;
using Hutech.Domain.Entities;

namespace Hutech.Application.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryRequest>()
            .ReverseMap();

        CreateMap<Category, CategoryResponse>()
            .ReverseMap();
    }
}