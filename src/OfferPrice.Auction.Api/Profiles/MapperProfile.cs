using AutoMapper;
using OfferPrice.Auction.Api.Models.Requests;
using OfferPrice.Auction.Api.Models.Responses;
using OfferPrice.Auction.Application.LocationOperations.CreateLocation;
using OfferPrice.Auction.Application.LotOperations.CreateLot;
using OfferPrice.Auction.Application.LotOperations.GetLot;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Api.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LotRequest, CreateLotCommand>();
            CreateMap<Lot, Domain.Models.Lot>().ReverseMap();
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, Domain.Models.Product>();
            
            CreateMap<Lot, GetLotResponse>();

            CreateMap<FindRegularLotsRequest, GetRegularLotsCommand>();
            CreateMap<FindUserLotsRequest, GetUserLotsCommand>();

            CreateMap<Category, Domain.Models.Category>().ReverseMap();

            CreateMap<Location, Domain.Models.Location>().ReverseMap();
            CreateMap<CreateLocationRequest, CreateLocationCommand>();
        }
    }
}
