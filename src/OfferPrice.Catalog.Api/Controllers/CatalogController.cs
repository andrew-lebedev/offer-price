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

    [HttpGet]
    public async Task<IActionResult> GetProducts
        ([FromQuery] GetProductsRequest productRequest, CancellationToken token)
    {
        var page = await _database.GetProducts(productRequest.Name,
                                        productRequest.Username,
                                        productRequest.Category,
                                        productRequest.Page,
                                        productRequest.PerPage,
                                        token);

        var products = page.Items.Select(_mapper.Map<Domain.Product, Models.Product>).ToList();

        var pageResult = new PageResult<Models.Product>(page.Page, page.PerPage, page.TotalPages, products);

        return Ok(page);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] string id, CancellationToken token)
    {
        var product = await _database.GetProductById(id, token);

        var productResponse = _mapper.Map<Domain.Product, Models.Product>(product);

        return Ok(productResponse);
    }

    [HttpPost]
    public async Task<IActionResult> InsertProduct([FromBody] InsertProductRequest productRequest, CancellationToken token)
    {
        var product = _mapper.Map<InsertProductRequest, Domain.Product>(productRequest);

        await _database.InsertProduct(product, token);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdateProductRequest productRequest, CancellationToken token)
    {

        var product = await _database.GetProductById(id, token);

        if (product == null)
        {
            return NotFound();
        }

        var updatedProduct = _mapper.Map(productRequest, product);

        await _database.UpdateProduct(updatedProduct, token);
        return Ok();
    }

    [HttpPost("{id}/hide")]
    public async Task<IActionResult> HideProduct([FromRoute] string id, CancellationToken token)
    {
        var product = await _database.GetProductById(id, token);

        if (product == null)
        {
            return NotFound();
        }

        if (product.Status == "hidden")
        {
            return Conflict();
        }
        else
        {
            product.Status = "hidden";
        }

        await _database.UpdateProduct(product, token);

        return Ok();
    }

    [HttpPost("{id}/show")]
    public async Task<IActionResult> ShowProduct([FromRoute] string id, CancellationToken token)
    {
        var product = await _database.GetProductById(id, token);

        if (product == null)
        {
            return NotFound();
        }

        if (product.Status == "observable")
        {
            return Conflict();
        }
        else
        {
            product.Status = "observable";
        }

        await _database.UpdateProduct(product, token);

        return Ok();
    }
}

