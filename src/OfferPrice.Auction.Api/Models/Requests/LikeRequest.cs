using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OfferPrice.Auction.Api.Models.Requests;

public class LikeRequest
{
    public string UserId { get; set; }

    public string LotId { get; set; }
}
