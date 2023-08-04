using DataIntensive.Api.Application.Payments.PayInvoice;
using MediatR;

namespace DataIntensive.Api.Application.Payments.CreatePayment
{
    public class PayInvoiceCommand : IRequest
    {
        public Guid UserId { get; set; }

        public string CardNumber { get; set; }

        public string CardCvv { get; set; }

        public DateTime CardExpirationDate { get; set; }

        public string CardOwner { get; set; }

        public string CardOwnerDocument { get; set; }

        public decimal PaymentValue { get; set; }

        public PayInvoiceProductsDto[] Items { get; set; }

        public PayInvoiceCommand(Guid userId,
            string cardNumber,
            string cardCvv,
            DateTime cardExpirationDate,
            string cardOwner,
            string cardOwnerDocument,
            decimal paymentValue,
            PayInvoiceProductsDto[] items)
        {
            UserId = userId;
            CardNumber = cardNumber;
            CardCvv = cardCvv;
            CardExpirationDate = cardExpirationDate;
            CardOwner = cardOwner;
            CardOwnerDocument = cardOwnerDocument;
            PaymentValue = paymentValue;
            Items = items;
        }
    }
}