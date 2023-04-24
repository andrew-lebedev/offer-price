using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OfferPrice.Catalog.Api.DataService;
using OfferPrice.Catalog.Api.Models;

namespace OfferPrice.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly IDatabaseService _database;

        public CatalogController(IDatabaseService database)
        {
            _database = database;
        }

        [HttpGet("products")]
        public async Task<IResult> GetProducts([FromQuery] string name, [FromQuery] string username, [FromQuery] string category)
        {
            var products = await _database.GetProducts(name, username, category);

            return Results.Ok(products);
        }

        [HttpGet("products/{id}")]
        public async Task<IResult> GetProductById([FromRoute] string id)
        {
            var product = await _database.GetProductById(id);

            return Results.Ok(product);
        }

        [HttpPost("products")]
        public async Task<IResult> InsertProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return Results.BadRequest();
            }

            await _database.InsertProduct(product);

            return Results.Ok();
        }

        [HttpPut("products/{id}")]
        public async Task<IResult> UpdateProduct([FromRoute] string id, [FromBody] Product product)
        {
            if (product == null)
            {
                return Results.BadRequest();
            }

            await _database.UpdateProduct(id, product);

            return Results.Ok();
        }

        [HttpPost("products/{id}/hide")]
        public async Task<IResult> HideProduct([FromRoute] string id)
        {
            await _database.HideProduct(id);

            return Results.Ok();
        }

        [HttpPost("products/{id}/show")]
        public async Task<IResult> ShowProduct([FromRoute] string id)
        {
            await _database.ShowProduct(id);

            return Results.Ok();
        }
    }
}
