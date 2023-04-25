using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Catalog.Api.Models;
using OfferPrice.Catalog.Domain;

namespace OfferPrice.Catalog.Api.Controllers;

[ApiController]
[Route("api/products")]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _database;
    private readonly IMapper _mapper;

    public CatalogController(IProductRepository database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetProducts([FromQuery] string name, [FromQuery] string username, [FromQuery] string category)
    {
        var products = await _database.GetProducts(name, username, category);

        var productsResponse = products.Select(_mapper.Map<Product, ProductResponse>).ToList();

        return Ok(productsResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] string id)
    {
        var product = await _database.GetProductById(id);

        var productResponse = _mapper.Map<Product, ProductResponse>(product);

        return Ok(productResponse);
    }

    [HttpPost("")]
    public async Task<IActionResult> InsertProduct([FromBody] ProductRequest productRequest)
    {
        var product = _mapper.Map<ProductRequest, Product>(productRequest, new Product());

        await _database.InsertProduct(product);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] ProductRequest productRequest)
    {
        var product = _mapper.Map<ProductRequest, Product>(productRequest, new Product(id));

        var prod = await _database.UpdateProduct(id, product);

        if (prod == null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost("{id}/hide")]
    public async Task<IActionResult> HideProduct([FromRoute] string id)
    {
        var product = await _database.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        product.Status = "hidden";

        await _database.UpdateProduct(id, product);

        return Ok();
    }

    [HttpPost("{id}/show")]
    public async Task<IActionResult> ShowProduct([FromRoute] string id)
    {
        var product = await _database.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        product.Status = "observable";

        await _database.UpdateProduct(id, product);

        return Ok();
    }
}

