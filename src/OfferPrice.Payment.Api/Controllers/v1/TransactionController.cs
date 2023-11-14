using Microsoft.AspNetCore.Mvc;
using OfferPrice.Payment.Domain;

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
    public async Task<IActionResult> GetUserTransactions()
    {

        return Ok();
    }
}


