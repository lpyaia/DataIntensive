using DataIntensive.Api.Domain.Users;
using DataIntensive.Api.Infrastructure.Data.EF.Maps;

namespace DataIntensive.Api.Domain.Orders
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        public string CardNumber { get; set; } = default!;

        public string CardCvv { get; set; } = default!;

        public DateTime CardExpirationDate { get; set; }

        public string CardOwner { get; set; } = default!;

        public string CardOwnerDocument { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public decimal Total { get; set; }

        public User User { get; set; } = default!;

        public List<OrderItem> Items { get; set; } = new();

        private Order()
        { }

        public Order(Guid userId,
            string cardNumber,
            string cardCvv,
            DateTime cardExpirationDate,
            string cardOwner,
            string cardOwnerDocument,
            decimal total,
            List<OrderItem> items)
        {
            var totalItems = items.Sum(x => x.Product.Price * x.Quantity);

            if (total != totalItems)
                throw new InvalidOrderPriceException("Invalid price!");

            UserId = userId;
            CardNumber = cardNumber;
            CardCvv = cardCvv;
            CardExpirationDate = cardExpirationDate;
            CardOwner = cardOwner;
            CardOwnerDocument = cardOwnerDocument;
            Total = total;
            CreatedAt = DateTime.UtcNow;
            Items.AddRange(items);
        }
    }
}