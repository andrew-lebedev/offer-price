using AutoMapper;
using OfferPrice.Auction.Api.Models.Requests;
using OfferPrice.Auction.Api.Models.Responses;
using OfferPrice.Auction.Application.LocationOperations.CreateLocation;
using OfferPrice.Auction.Application.LotOperations.CreateLot;
using OfferPrice.Auction.Application.LotOperations.GetLot;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Common;

namespace OfferPrice.Auction.Api.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LotRequest, CreateLotCommand>()
                .ForMember(x => x.InsertProduct, y => y.MapFrom(k => k.InsertProductRequest));

            CreateMap<CreateLotCommand, Domain.Models.Lot>()
                .ForMember(x => x.Product, y => y.MapFrom(k => k.InsertProduct))
                .ForMember(x => x.CurrentPrice, y => y.MapFrom(k => k.InsertProduct.Price));
            CreateMap<Lot, Domain.Models.Lot>().ReverseMap();

            CreateMap<ProductRequest, Product>();
            CreateMap<Product, Domain.Models.Product>().ReverseMap();

            CreateMap<Lot, GetLotResponse>();

            CreateMap<PageResult<Lot>, PageResult<Domain.Models.Lot>>().ReverseMap();

            CreateMap<User, Domain.Models.User>().ReverseMap();

            CreateMap<FindRegularLotsRequest, GetRegularLotsCommand>();
            CreateMap<FindUserLotsRequest, GetUserLotsCommand>();

            CreateMap<Category, Domain.Models.Category>().ReverseMap();

            CreateMap<Location, Domain.Models.Location>().ReverseMap();
            CreateMap<CreateLocationRequest, CreateLocationCommand>();
        }
    }
}
