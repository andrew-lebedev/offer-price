using AutoMapper;
using OfferPrice.Auction.Api.Models;
using OfferPrice.Auction.Api.Models.Requests;

namespace OfferPrice.Auction.Api.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Domain.Models.Lot, Lot>();

            CreateMap<Domain.Models.Bet, Bet>();

            CreateMap<ProductRequest, Domain.Models.Product>();

            CreateMap<LotRequest, Domain.Models.Lot>();
        }
    }
}
