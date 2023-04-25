using AutoMapper;
using OfferPrice.Catalog.Api.Models;

namespace OfferPrice.Catalog.Api.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductRequest>();
            CreateMap<ProductRequest, Product>();
        }
    }
}
