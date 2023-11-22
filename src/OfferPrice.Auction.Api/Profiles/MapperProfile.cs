using AutoMapper;
using OfferPrice.Auction.Api.Models;
using OfferPrice.Auction.Domain.Models;

namespace OfferPrice.Auction.Api.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Domain.Models.Lot, Lot>();

            CreateMap<Domain.Models.Bet, Bet>();
        }
    }
}
