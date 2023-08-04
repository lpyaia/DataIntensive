using DataIntensive.Api.Application.Payments.CreatePayment;
using DataIntensive.Api.Domain.Orders;
using DataIntensive.Api.Domain.Products;
using DataIntensive.Api.Infrastructure.Data.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataIntensive.Api.Application.Payments.PayInvoice
{
    public class PayInvoiceCommandHandler : IRequestHandler<PayInvoiceCommand>
    {
        private readonly DataIntensiveEntityFrameworkContext _context;

        public PayInvoiceCommandHandler(DataIntensiveEntityFrameworkContext context)
        {
            _context = context;
        }

        public async Task Handle(PayInvoiceCommand request, CancellationToken cancellationToken)
        {
            var productIds = request.Items.Select(x => x.ProductId);
            var products = await _context.Set<Product>().Where(x => productIds.Contains(x.Id)).ToListAsync();

            OrderBuilder orderBuilder = OrderBuilder.CreateOrder(request.UserId,
                                                                 request.CardNumber,
                                                                 request.CardCvv,
                                                                 request.CardExpirationDate,
                                                                 request.CardOwner,
                                                                 request.CardOwnerDocument,
                                                                 request.PaymentValue);

            foreach (var item in request.Items)
            {
                var product = products.First(x => x.Id == item.ProductId);
                orderBuilder.AddItem(product, item.Quantity);
            }

            var order = orderBuilder.Build();

            _context.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}