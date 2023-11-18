using Microsoft.AspNetCore.Mvc;
using OfferPrice.Common;
using OfferPrice.Payment.Api.Models;
using OfferPrice.Payment.Domain.Interfaces;
using OfferPrice.Payment.Domain.Models;

namespace OfferPrice.Payment.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/transaction/")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserTransactions([FromQuery] TransactionsRequest request, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var result = await _transactionRepository.Find(request.ToQuery(userId), token);

        return Ok(result);
    }

    [HttpPost("pay")]
    public async Task<IActionResult> Pay([FromBody] PaymentRequest request, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        //Create a response to WebMoney

        var transaction = new Transaction()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            AuctionId = request.AuctionId,
            AuthCode = "",
            Rnn = "",
            Date = DateTime.Now,
            Status = "Done",
            Price = request.Price
        };

        await _transactionRepository.Create(transaction, token);

        return Ok();
    }
}


