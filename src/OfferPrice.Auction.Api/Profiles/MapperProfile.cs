using AutoMapper;
using OfferPrice.Auction.Api.Models;

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
