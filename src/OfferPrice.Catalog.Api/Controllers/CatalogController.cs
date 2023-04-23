using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OfferPrice.Catalog.Api.Models;

namespace OfferPrice.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Product> _products;

        public CatalogController(IMongoDatabase database)
        {
            _database = database;

            _products = _database.GetCollection<Product>("Products");
        }


        [HttpGet("products")]
        public async Task<IResult> GetProducts()
        {
            var products = await
                (await _products.FindAsync(Builders<Product>.Filter.Where(x => true)))
                .ToListAsync(); 

            return Results.Ok(products);
        }

        [HttpGet("products/{id}")]
        public async Task<IResult> GetProductById([FromRoute] string id)
        {
            var product = await (await _products.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();

            return Results.Ok(product);
        }

        [HttpPost("products")]
        public async Task<IResult> InsertProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return Results.BadRequest();
            }

            await _products.ReplaceOneAsync(item => item.Id == product.Id, product, new ReplaceOptions { IsUpsert = true });

            return Results.Ok();
        }

        [HttpPut("products/{id}")]
        public async Task<IResult> UpdateProduct([FromRoute] string id, [FromBody] Product product)
        {
            if (product == null)
            {
                return Results.BadRequest();
            }

            var prod = await _products.UpdateOneAsync(
                Builders<Product>.Filter.Where(x => x.Id == id),
                Builders<Product>.Update.Set(x => x.Name, product.Name)
                                        .Set(x => x.Category, product.Category)
                                        .Set(x => x.Description, product.Description)
                                        .Set(x => x.User, product.User)
                                        .Set(x => x.Price, product.Price)
                                        .Set(x => x.Status, product.Status),
                new UpdateOptions { IsUpsert = true }
                );

            return Results.Ok(prod);
        }

        [HttpPost("products/{id}/hide")]
        public async Task<IResult> HideProduct([FromRoute] string id)
        {
            await _products.UpdateOneAsync(
                Builders<Product>.Filter.Where(x => x.Id == id),
                Builders<Product>.Update.Set(x => x.Status, "hidden")
                );

            return Results.Ok();
        }

        [HttpPost("products/{id}/show")]
        public async Task<IResult> ShowProduct([FromRoute] string id)
        {
            await _products.UpdateOneAsync(
                Builders<Product>.Filter.Where(x => x.Id == id),
                Builders<Product>.Update.Set(x => x.Status, "observable")
                );

            return Results.Ok();
        }
    }
}
