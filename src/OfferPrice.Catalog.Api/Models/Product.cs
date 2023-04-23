using MongoDB.Bson.Serialization.Attributes;

namespace OfferPrice.Catalog.Api.Models
{
    public class Product
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string User { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

        public Product()//Guid?
        {

        }
    }
}
