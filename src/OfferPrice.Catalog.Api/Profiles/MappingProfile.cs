using AutoMapper;
using OfferPrice.Catalog.Api.Models;
using OfferPrice.Catalog.Domain;

namespace OfferPrice.Catalog.Api.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductRequest>();
        CreateMap<ProductRequest, Product>();

        CreateMap<Product, ProductResponse>();
        CreateMap<ProductResponse, Product>();
    }
}

