using AutoMapper;
using OfferPrice.Auction.Api.Models;

namespace OfferPrice.Auction.Api.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Domain.Lot, Lot>();

            CreateMap<Domain.Bet, Bet>();
        }
    }
}
