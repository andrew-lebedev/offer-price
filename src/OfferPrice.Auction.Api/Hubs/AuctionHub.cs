using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using OfferPrice.Auction.Api.ConnectionMapping;
using OfferPrice.Auction.Domain;
using SignalRSwaggerGen.Attributes;

namespace OfferPrice.Auction.Api.Hubs;

[SignalRHub]
public class AuctionHub : Hub
{
    private readonly IBetRepository _bets;

    private readonly static ConnectionsMapper<string> _connectionsMapper = new ConnectionsMapper<string>();

    private readonly IMapper _mapper;

    public AuctionHub(IBetRepository bets, IMapper mapper)
    {
        _bets = bets;
        _mapper = mapper;
    }

    public async Task RaiseBet(Models.BetRequest betRequest)
    {
        var bet = _mapper.Map<Bet>(betRequest);

        await _bets.Create(bet);

        var betResponse = _mapper.Map<Models.Bet>(bet);

        var connections = _connectionsMapper.GetAuctionConnections(betResponse.Auction.Id);

        if (connections != null)
        {
            foreach (var connection in connections)
            {
                await Clients.Client(connection).SendAsync("raise_bet", betResponse);
            }
        }


    }

    public override async Task OnConnectedAsync()
    {
        var auctionId = Context.GetHttpContext().Request.Query["auction-id"]; // /auctionHub?auction-id={auction-id}

        _connectionsMapper.AddConnection(auctionId, Context.ConnectionId);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var auctionId = Context.GetHttpContext().Request.Query["auction-id"];

        _connectionsMapper.RemoveConnection(auctionId, Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);
    }
}

