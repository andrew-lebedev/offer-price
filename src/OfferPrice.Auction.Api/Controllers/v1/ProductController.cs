using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Api.Models;
using OfferPrice.Auction.Api.Models.Requests;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Common;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    private IMapper _mapper;

    public ProductController(IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var products = await _productRepository.GetUserProducts(userId, cancellationToken);

        return Ok(products);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct([FromRoute] string productId, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var product = await _productRepository.GetUserProductById(productId, userId, cancellationToken);

        var productResponse = _mapper.Map<Product>(product);

        return Ok(productResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] InsertProductRequest insertProductRequest, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);
        var user = await _userRepository.Get(userId, cancellationToken);
        
        if (user == null)
        {
            return Conflict();
        }

        return Ok();
    }
}

