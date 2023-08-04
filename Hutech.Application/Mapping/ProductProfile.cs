﻿using AutoMapper;
using Hutech.Application.Models;
using Hutech.Domain.Entities;

namespace Hutech.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ReverseMap();

        CreateMap<Product, ProductRequest>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ReverseMap();
    }
}