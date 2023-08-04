using DataIntensive.Api.Domain.Products;

namespace DataIntensive.Api.Domain.Orders
{
    public class OrderBuilder
    {
        public readonly Guid _userId;
        private readonly string _cardNumber;
        private readonly string _cardCvv;
        private readonly DateTime _cardExpirationDate;
        private readonly string _cardOwner;
        private readonly string _cardOwnerDocument;
        private readonly decimal _total;
        private readonly List<OrderItem> _items;

        public OrderBuilder(Guid userId,
            string cardNumber,
            string cardCvv,
            DateTime cardExpirationDate,
            string cardOwner,
            string cardOwnerDocument,
            decimal total)
        {
            _userId = userId;
            _cardNumber = cardNumber;
            _cardCvv = cardCvv;
            _cardExpirationDate = cardExpirationDate;
            _cardOwner = cardOwner;
            _cardOwnerDocument = cardOwnerDocument;
            _total = total;
            _items = new List<OrderItem>();
        }

        public static OrderBuilder CreateOrder(Guid userId,
            string cardNumber,
            string cardCvv,
            DateTime cardExpirationDate,
            string cardOwner,
            string cardOwnerDocument,
            decimal total)
        {
            return new OrderBuilder(userId,
                cardNumber,
                cardCvv,
                cardExpirationDate,
                cardOwner,
                cardOwnerDocument,
                total);
        }

        public OrderBuilder AddItem(Product product, int quantity)
        {
            _items.Add(new OrderItem(product, quantity));

            return this;
        }

        public Order Build()
        {
            return new Order(_userId,
                _cardNumber,
                _cardCvv,
                _cardExpirationDate,
                _cardOwner,
                _cardOwnerDocument,
                _total,
                _items);
        }
    }
}