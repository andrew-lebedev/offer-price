﻿using AutoMapper;
using OfferPrice.Catalog.Api.Models;

namespace OfferPrice.Catalog.Api.Profiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<InsertProductRequest, Domain.Product>();
        CreateMap<UpdateProductRequest, Domain.Product>();
        CreateMap<Domain.Product, Product>();
    }
}
