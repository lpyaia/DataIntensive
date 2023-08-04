using DataIntensive.Api.Infrastructure.Data.EF.Maps;

namespace DataIntensive.Api.Domain.Products
{
    public class Product : BaseEntity
    {
        public string Description { get; set; } = default!;

        public decimal Price { get; set; }
    }
}