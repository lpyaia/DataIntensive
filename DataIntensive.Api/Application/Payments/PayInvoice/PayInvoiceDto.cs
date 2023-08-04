namespace DataIntensive.Api.Application.Payments.PayInvoice
{
    public class PayInvoiceDto
    {
        public Guid UserId { get; set; }

        public string CardNumber { get; set; } = default!;

        public string CardCvv { get; set; } = default!;

        public DateTime CardExpirationDate { get; set; }

        public string CardOwner { get; set; } = default!;

        public string CardOwnerDocument { get; set; } = default!;

        public decimal PaymentValue { get; set; }

        public PayInvoiceProductsDto[] Items { get; set; } = default!;
    }
}