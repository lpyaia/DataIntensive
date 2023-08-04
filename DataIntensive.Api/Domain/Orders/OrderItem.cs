using DataIntensive.Api.Domain.Products;

namespace DataIntensive.Api.Domain.Orders
{
    public class OrderItem
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; } = default!;

        public Product Product { get; set; } = default!;

        private OrderItem()
        { }

        public OrderItem(Product product, int quantity)
        {
            ProductId = product.Id;
            Product = product;
            Quantity = quantity;
        }
    }
}