using AutoMapper;
using OfferPrice.Auction.Api.Models;

namespace OfferPrice.Auction.Api.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AuctionRequest, Domain.Lot>();

            CreateMap<Domain.Lot, Models.Lot>();

            CreateMap<BetRequest, Domain.Bet>();

            CreateMap<Domain.Bet, Models.Bet>();
        }
    }
}
