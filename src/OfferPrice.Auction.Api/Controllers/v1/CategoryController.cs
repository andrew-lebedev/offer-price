using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Application.CategoryOperations.CreateCategory;
using OfferPrice.Auction.Application.CategoryOperations.GetCategory;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categories")]
public class CategoryController : Controller
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var cmd = new GetCategoriesCommand();

        var categories = await _mediator.Send(cmd, cancellationToken);

        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string name, CancellationToken cancellationToken)
    {
        var cmd = new CreateCategoryCommand() { Name = name };

        await _mediator.Send(cmd, cancellationToken);

        return Ok();
    }
}
